using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "Data/ThemeData")]
public class ThemeData : ScriptableObject
{
    public string ThemeName;
    public int MaxObstacleSpawnRate;
    public int MaxIncursionObjectRate;

    [Header("All Tile")]
    public List<TileData> TileDatas;

    [Space]
    [Header("All Objects")]
    public ObjectData[] ObjectDatas;

#if UNITY_EDITOR
    private void OnValidate()
    {
        TileDatas.Last().TileSpawnRate = 100;
        TileDatas.Last().IncursionRate = 50;

        if (ObjectDatas.Length <= 0) return;

        foreach(var _obj in ObjectDatas)
        {
            if(_obj.ObjectSpawnRate > MaxObstacleSpawnRate)
                _obj.ObjectSpawnRate = MaxObstacleSpawnRate;

            if (_obj.IncursionRate > MaxIncursionObjectRate)
                _obj.IncursionRate = MaxIncursionObjectRate;
        }
    }
#endif

}

[System.Serializable]
public class ObjectData
{
    public string ID;
    public string TileCompatible_ID;
    public int IncursionRate;
    public int ObjectSpawnRate;
    public GameObject Object;
    public ObjectBase ObjectBase;

    public void SetObjectBase() => ObjectBase = Object.GetComponent<ObjectBase>();
}

[System.Serializable]
public class TileData
{
    [Header("Identity")]
    public string ID;
    public string BiomeID;

    [Header("Rate : Max 100")]
    public int TileSpawnRate;
    public int IncursionRate;
    public int MaxSpawn;

    [Header("Setting")]
    public Sprite TileSprite;
    public bool Walkable = true;

    [Header("Biome Setting : Max 100")]
    public int BiomeSize;
    public int BiomeChance;

    [Header("For Surround Tile")]
    public float SurroundRange;
    public SurroundTileData[] SurroundDatas;

    [Header("On Tile : Max 100")]
    public int ObjectSpawnChance;
    public GameObject ObjectOnTile;
}

[System.Serializable]
public class SurroundTileData
{
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left,
        Up_Right,
        Up_Left,
        Down_Right,
        Down_Left
    }

    public Vector3 GetVector()
    {
        Vector3 _newVector = new Vector3(0, 0, 0);
        switch (SetDirection)
        {
            case Direction.Up :
                _newVector = Vector3.forward;
                break;

            case Direction.Down:
                _newVector = Vector3.back;
                break;

            case Direction.Right:
                _newVector = Vector3.right;
                break;

            case Direction.Left:
                _newVector = Vector3.left;
                break;

            case Direction.Up_Right:
                _newVector = (Vector3.forward + Vector3.right).normalized;
                break;

            case Direction.Up_Left:
                _newVector = (Vector3.forward + Vector3.left).normalized;
                break;

            case Direction.Down_Right:
                _newVector = (Vector3.back + Vector3.right).normalized;
                break;

            case Direction.Down_Left:
                _newVector = (Vector3.back + Vector3.left).normalized;
                break;
        }

        return _newVector;
    }

    public Direction SetDirection;
    public Sprite Sprite;
    public CorrectTileData[] CorrectTileDatas;
}

[System.Serializable]
public class CorrectTileData
{
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left,
        Up_Right,
        Up_Left,
        Down_Right,
        Down_Left
    }
    public Direction CorrectTileDirection;
    public Sprite CorrectSprite;
}