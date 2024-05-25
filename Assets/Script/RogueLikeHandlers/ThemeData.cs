using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

            if (_obj.MaxIncursion > MaxIncursionObjectRate)
                _obj.MaxIncursion = MaxIncursionObjectRate;
        }
    }
#endif

}

[System.Serializable]
public class ObjectData
{
    public string ID;
    public string TileCompatible_ID;
    public int ObjectSpawnRate;
    public GameObject Object;
    public ObjectBase ObjectBase;
    public int MaxIncursion;

    public void SetObjectBase() => ObjectBase = Object.GetComponent<ObjectBase>();
}

[System.Serializable]
public class TileData
{
    public string ID;
    public int TileSpawnRate;
    public int IncursionRate;
    public Sprite TileSprite;
    public bool Walkable = true;
}
