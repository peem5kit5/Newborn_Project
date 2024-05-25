using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class WorldGenerator : MonoBehaviour
{
    [Header("Setting")]
    public int Width = 10;
    public int Height = 10;
    public Vector3 Offset = new Vector3(1.5f, 0, 1.5f);

    [Header("Reference")]
    [SerializeField] private Transform parent_ToSpawn;
    [SerializeField] private GameObject tileBase;
    private ThemeData themeData;

    [Header("See Only")]
    [SerializeField] private int seed;

    [Header("Tile (See Only)")]
    [SerializeField] private TilePattern currentTilePattern_Creating;
    [SerializeField] private bool isCurrentTile_Incursion;

    [Header("Object (See Only)")]
    [SerializeField] private ObjectBase currentObjectBase_Creating;
    [SerializeField] private bool isCurrentObject_InCursion;


    private void OnValidate()
    {
        if (!tileBase)
        {
            string _folderPath = "Assets/Prefabs/TileBase.prefab";
            tileBase = AssetDatabase.LoadAssetAtPath<GameObject>(_folderPath);
        }
    }

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
                if (!isCurrentTile_Incursion)
                    CreateTile(_pos);
                else
                    CreateTile_Incursion(_pos);

                if (!currentTilePattern_Creating)
                {
                    if (currentTilePattern_Creating.Walkable)
                    {

                    }
                }
            }
        }
    }

    private void CreateTile(Vector3 _pos)
    {
        var _tile = Instantiate(tileBase, _pos, Quaternion.identity).GetComponent<TilePattern>();
        _tile.Init(this);
        currentTilePattern_Creating = _tile;

        var _tileDatasTheme = themeData.TileDatas;

        for(int i = 0; i < _tileDatasTheme.Count; i++)
        {
            if(i == _tileDatasTheme.Count)
            {
                _tile.SetTile(_tileDatasTheme[i].ID + "_" +_pos.x.ToString(), _tileDatasTheme[i].TileSprite, _tileDatasTheme[i].Walkable, _tileDatasTheme[i].IncursionRate);
                _tile.GetComponent<Transform>().SetParent(parent_ToSpawn);
                break;
            }

            var _tileData = _tileDatasTheme[i];
            bool _isSpawn = Random.Range(0, 100) <= _tileData.TileSpawnRate;

            if (!_isSpawn) continue;

            _tile.SetTile(_tileData.ID + "_" + _pos.x.ToString(), _tileData.TileSprite, _tileData.Walkable, _tileData.IncursionRate);
            _tile.GetComponent<Transform>().SetParent(parent_ToSpawn);
            break;
        }
        
        isCurrentTile_Incursion = IsIncursion(_tile);

        if (!isCurrentObject_InCursion)
            ObjectCreatingLogic(currentTilePattern_Creating);
        else
            ObjectCreatingLogic_Incursion(currentTilePattern_Creating);

    }

    private void CreateTile_Incursion(Vector3 _pos)
    {
        if (!currentTilePattern_Creating)
        {
            Debug.LogError("Creating Tile Incursion Error.");
            return;
        }

        var _tile = Instantiate(currentTilePattern_Creating, _pos, Quaternion.identity).GetComponent<TilePattern>();
        _tile.transform.SetParent(parent_ToSpawn);

        isCurrentTile_Incursion = IsIncursion(_tile);
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

        if(_objectBase is ObstacleObject _obstacle)
        {
            if (!_obstacle.AllowToSpawn())
            {
                Destroy(_obstacle.gameObject);
                return;
            } 
        }

        isCurrentObject_InCursion = IsIncursion(_objectBase);
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