using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePattern : ObjectBase
{
    [Header("Reference")]
    [SerializeField] private WorldGenerator worldGen;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Status")]
    [SerializeField] private bool walkable;

    public bool Walkable => walkable;

    private void OnEnable()
    {
        if (!sprite)
            sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetTile(string _id, Sprite _sprite, bool _walkable, int _incursionRate)
    {
        ID = _id;
        gameObject.name = "Tile_" + _id;

        sprite.sprite = _sprite;
        walkable = _walkable;
        IncursionRate = _incursionRate;
    }

    public override void Init(WorldGenerator _worldGen)
    {
        worldGen = _worldGen;
    }

    public override void DestroyThisObject()
    {
        throw new System.NotImplementedException();
    }
}
