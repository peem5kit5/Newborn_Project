using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Unity.VisualScripting;
using static UnityEngine.UI.Image;

public class WorldGenerator : MonoBehaviour
{
    [Header("Setting")]
    public int Width = 10;
    public int Height = 10;
    public Vector3 Offset = new Vector3(1.5f, 0, 1.5f);

    [Header("Reference")]
    [SerializeField] private Transform parent_ToSpawn;
    [SerializeField] private GameObject tileBase;
    [SerializeField] private GameObject incursionMaker_prefab;
    private ThemeData themeData;

    [Header("See Only")]
    [SerializeField] private IncursionMaker incursionMaker;
    [SerializeField] private int seed;
    [SerializeField] private bool isPlayerInit = false;

    [Header("Tile (See Only)")]
    [SerializeField] private TilePattern currentTilePattern_Creating;
    [SerializeField] private List<TilePattern> tilePatterns = new List<TilePattern>();

    [Header("Object (See Only)")]
    [SerializeField] private ObjectBase currentObjectBase_Creating;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!tileBase)
        {
            string _folderPath = "Assets/Prefabs/TileBase.prefab";
            tileBase = AssetDatabase.LoadAssetAtPath<GameObject>(_folderPath);
        }

        if (!incursionMaker_prefab)
        {
            string _folderPath = "Assets/Script_Box/Game_Script/IncursionMaker.prefab";
            incursionMaker_prefab = AssetDatabase.LoadAssetAtPath<GameObject>(_folderPath);
        }
    }
#endif

    public void Init(ThemeData _themeData)
    {
        themeData = _themeData;
        GenerateWorld();
    }

    private void GenerateWorld()
    {
        GenerateSeed();
        WorldCreatingLogic();
    }

    private void WorldCreatingLogic()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Height; z++)
            {
                Vector3 _pos = new Vector3(x * Offset.x, 1, z * Offset.z);
                CreateTileBase(_pos);
            }
        }

        SettingTile();
    }

    private void SetPlayerPos(int x, int z)
    {
        if (isPlayerInit) return;

        if (currentTilePattern_Creating)
        {
            if (currentTilePattern_Creating.Walkable && x >= Width && z >= Height)
            {
                GameManager.Instance.Player.transform.position = currentTilePattern_Creating.transform.position;
                isPlayerInit = true;
                return;
            }

            if (currentTilePattern_Creating.Walkable)
            {
                int _randomChance = Random.Range(0, 100);
                if (_randomChance <= 30)
                {
                    GameManager.Instance.Player.transform.position = currentTilePattern_Creating.transform.position;
                    isPlayerInit = true;
                } 
            }
        }
    }

    private void CreateTileBase(Vector3 _pos)
    {
        var _tile = Instantiate(tileBase, _pos, Quaternion.identity).GetComponent<TilePattern>();
        _tile.Init(this);

        tilePatterns.Add(_tile);
    }

    private void SettingTile()
    {
        for(int i = 0; i < tilePatterns.Count; i++)
        {
            var _tile = tilePatterns[i];
            var _tileDatasTheme = themeData.TileDatas;

            for (int j = 0; j < _tileDatasTheme.Count; j++)
            {
                if (_tile.IsOverride) break;

                if (j == _tileDatasTheme.Count)
                {
                    var _tileDataLast = _tileDatasTheme[j];
                    _tile.SetTile(_tileDataLast);
                    _tile.GetComponent<Transform>().SetParent(parent_ToSpawn);
                    break;
                }

                var _tileData = _tileDatasTheme[j];
                bool _isSpawn = Random.Range(0, 100) <= _tileData.TileSpawnRate;

                if (!_isSpawn) continue;

                _tile.SetTile(_tileData);
                _tile.GetComponent<Transform>().SetParent(parent_ToSpawn);
                break;
            }

            if (IsIncursion(_tile))
                CreateTile_Incursion(_tile);

            if (_tile.TileData.SurroundDatas.Length > 0)
                _tile.SetSurround();
        }
    }

    private void CreateTile_Incursion(TilePattern _tile)
    {
        Collider[] _cols = Physics.OverlapSphere(_tile.transform.position, _tile.SurroundRange);

        foreach (var _col in _cols)
        {
            var _tilePattern = _col.GetComponent<TilePattern>();
            if (_tilePattern == null || _tilePattern == _tile) continue;

            _tilePattern.SetTileOverride(_tile);
        }
    }

    private void ObjectCreatingLogic(TilePattern _tilePattern)
    {
        var _objectDatasTheme = themeData.ObjectDatas;
        List<ObjectData> _objectDataList = new List<ObjectData>();

        foreach (var _object in _objectDatasTheme)
        {
            if (_object.TileCompatible_ID == _tilePattern.ID)
                _objectDataList.Add(_object);
        }

        for(int i = 0; i < _objectDataList.Count; i++)
        {
            var _objectData = _objectDataList[i];
            
            if(!_objectData.ObjectBase)
                _objectData.SetObjectBase();

            bool _isSpawn = Random.Range(0, 100) <= _objectData.ObjectSpawnRate;

            if (!_isSpawn) continue;

            ObjectBase _objectBase = Instantiate(_objectDataList[i].Object, _tilePattern.transform.position, Quaternion.identity).GetComponent<ObjectBase>();
            currentObjectBase_Creating = _objectBase;

            SetObject(_objectBase);
        }
    }

    private void ObjectCreatingLogic_Incursion(TilePattern _tilePattern)
    {
        if (!currentObjectBase_Creating)
        {
            Debug.Log("Creating Object Error.");
            return;
        }

        var _obj = Instantiate(currentObjectBase_Creating.gameObject, _tilePattern.transform.position, Quaternion.identity).GetComponent<ObjectBase>();
        SetObject(_obj);
    }

    private void SetObject(ObjectBase _objectBase)
    {
        _objectBase.transform.SetParent(parent_ToSpawn);
        _objectBase.Init(this);

        if (_objectBase is ObstacleObject _obstacle)
        {
            if (!_obstacle.AllowToSpawn())
            {
                Destroy(_obstacle.gameObject);
                return;
            }
        }
    }

    public Vector3 GetTilePosAndOffset()
    {
        Vector3 _pos = new Vector3(currentTilePattern_Creating.transform.position.x * Offset.x
                                   , 1, currentTilePattern_Creating.transform.position.z * Offset.z);
        return _pos;
    }
 
    private void GenerateSeed()
    {
        seed = (int)System.DateTime.Now.Ticks;
        Random.InitState(seed);
    }

    private bool IsIncursion(ObjectBase _objectBase)
    {
        int _randomValue = Random.Range(0, 100);
        Debug.Log(_randomValue <= _objectBase.IncursionRate);
        return _randomValue <= _objectBase.IncursionRate;
    }
}