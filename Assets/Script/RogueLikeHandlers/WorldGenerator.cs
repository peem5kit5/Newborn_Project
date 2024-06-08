using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Unity.VisualScripting;
using static UnityEngine.UI.Image;
using TMPro;

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

    private Dictionary<string, TileData> tileCounting = new Dictionary<string, TileData>();

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
                tilePatterns.Add(CreateTileBase(_pos));
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

    private TilePattern CreateTileBase(Vector3 _pos)
    {
        var _tile = Instantiate(tileBase, _pos, Quaternion.identity).GetComponent<TilePattern>();
        _tile.Init(this);

        return _tile;
    }

    private void SettingTile()
    {
        Dictionary<string, int> _tileCounter = new Dictionary<string, int>();
        List<TilePattern> _tilePatternList = new List<TilePattern>();

        for (int i = tilePatterns.Count - 1; i >= 0; i--)
        {
            int _randomIndex = Random.Range(0, i + 1);
            var _tile = tilePatterns[_randomIndex];
            tilePatterns[_randomIndex] = tilePatterns[i];
            tilePatterns[i] = _tile;

            var _tileDatasTheme = themeData.TileDatas;

            foreach(var _tileData in _tileDatasTheme)
            {
                if (_tileCounter.TryGetValue(_tileData.ID, out int _counter))
                {
                    bool _isMaxedSpawn = _counter > _tileData.MaxSpawn;
                    bool _isLimited = _tileData.MaxSpawn != 0;
                    if (_isMaxedSpawn && _isLimited)
                    {
                        if (_counter > _tileData.MaxSpawn)
                            continue;
                    }
                }

                if (_tileData == _tileDatasTheme.Last())
                {
                    _tile.SetTile(_tileData);
                    _tile.GetComponent<Transform>().SetParent(parent_ToSpawn);
                    _tilePatternList.Add(_tile);

                    break;
                }

                bool _isSpawn = Random.Range(0, 100) <= _tileData.TileSpawnRate;

                if (!_isSpawn) continue;

                _tile.SetTile(_tileData);
                _tile.GetComponent<Transform>().SetParent(parent_ToSpawn);
                _tilePatternList.Add(_tile);

                break;
            }

            if (_tile.TileData.MaxSpawn > 0)
            {
                if (!_tileCounter.ContainsKey(_tile.ID))
                    _tileCounter.TryAdd(_tile.ID, 0);
                else
                    _tileCounter[_tile.ID]++;
            }

            currentTilePattern_Creating = _tile;
            tilePatterns.Remove(_tile);
        }

        foreach (var _tile in _tilePatternList)
        {
            //if (IsIncursion(_tile))
            //    CreateTile_Incursion(_tile);

            _tile.SetTileBiome(_tile.TileData.BiomeSize);

            if (_tile.TileData.ObjectOnTile)
            {
                bool _spawnOnTile = Random.Range(0, 100) <= _tile.TileData.ObjectSpawnChance;

                if (_spawnOnTile)
                    Instantiate(_tile.TileData.ObjectOnTile, _tile.transform);
            }
        }

        //tilePatterns.Clear();
        ////_tilePatternList.Clear();
    }

    private void CreateTile_Incursion(TilePattern _tile)
    {
        Collider[] _cols = Physics.OverlapSphere(_tile.transform.position, _tile.SurroundRange);

        foreach (var _col in _cols)
        {
            var _tilePattern = _col.GetComponent<TilePattern>();
            if (_tilePattern == null || _tilePattern == _tile) continue;

            _tilePattern.OverrideTile(_tile);
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
        return _randomValue <= _objectBase.IncursionRate;
    }
}