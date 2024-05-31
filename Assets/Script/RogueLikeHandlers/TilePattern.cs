using System.Collections.Generic;
using UnityEngine;

public class TilePattern : ObjectBase
{
    public SpriteRenderer Sprite => sprite;
    public bool Walkable => walkable;
    public bool IsOverride => isOverride;

    public float SurroundRange => surroundRange;

    public TileData TileData;

    [Header("Reference")]
    [SerializeField] private WorldGenerator worldGen;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Status")]
    [SerializeField] private bool walkable;
    [SerializeField] private bool isSurround;
    [SerializeField] private bool isOverride;
    [SerializeField] private float surroundRange;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(this.transform.position, surroundRange);
    }

    private void OnEnable()
    {
        if (!sprite)
            sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetSprite(Sprite _sprite)
    {
        isOverride = true;
        sprite.sprite = _sprite;
    }

    public void SetTile(TileData _tileData)
    {
        TileData = _tileData;

        ID = _tileData.ID;
        gameObject.name = "Tile_" + _tileData.ID;

        sprite.sprite = _tileData.TileSprite;
        walkable = _tileData.Walkable;
        surroundRange = _tileData.SurroundRange;

        IncursionRate = _tileData.IncursionRate;
    }

    public void SetTileOverride(TilePattern _tilePattern ,bool _isOverride = true)
    {
        isOverride = _isOverride;

        TileData = _tilePattern.TileData;

        ID = "OverridedTile_" + _tilePattern.ID;
        gameObject.name = "OverridedTile_" + _tilePattern.ID;

        sprite.sprite = _tilePattern.Sprite.sprite;
        walkable = _tilePattern.Walkable;
        surroundRange = _tilePattern.SurroundRange;

        IncursionRate = _tilePattern.IncursionRate;
    }

    private static readonly Dictionary<SurroundTileData.Direction, Vector3Int> directionVectors = new Dictionary<SurroundTileData.Direction, Vector3Int>
    {
        { SurroundTileData.Direction.Up, new Vector3Int(0, 0, 1) },
        { SurroundTileData.Direction.Up_Right, new Vector3Int(1, 0, 1) },
        { SurroundTileData.Direction.Right, new Vector3Int(1, 0 ,0) },
        { SurroundTileData.Direction.Down_Right, new Vector3Int(1, 0, -1) },
        { SurroundTileData.Direction.Down, new Vector3Int(0, 0, -1) },
        { SurroundTileData.Direction.Down_Left, new Vector3Int(-1, 0, -1) },
        { SurroundTileData.Direction.Left, new Vector3Int(-1, 0, 0) },
        { SurroundTileData.Direction.Up_Left, new Vector3Int(-1, 0, 1) }
    };

    public void SetSurround()
    {
        Vector3Int _tilePosition = new Vector3Int(Mathf.RoundToInt(transform.position.x), 1 ,Mathf.RoundToInt(transform.position.z));

        foreach (Vector3Int _direction in directionVectors.Values)
        {
            Vector3Int _neighborPosition = _tilePosition + _direction * 10;
            Collider[] _hitColliders = Physics.OverlapSphere(_neighborPosition, 1);
            if (_hitColliders.Length > 0)
            {
                foreach (var hitCollider in _hitColliders)
                {
                    TilePattern _neighborTile = hitCollider.GetComponent<TilePattern>();
                    if (_neighborTile != null)
                    {
                        foreach (SurroundTileData _surroundTile in TileData.SurroundDatas)
                        {
                            if (directionVectors.ContainsKey(_surroundTile.SetDirection))
                            {
                                Debug.Log($"Setting sprite for tile at direction: {_surroundTile.SetDirection}");
                                _neighborTile.SetSprite(_surroundTile.Sprite);
                            }
                        }
                    }
                    else
                    {
                        Debug.Log($"No TilePattern component found on collider at {_neighborPosition}.");
                    }
                }
            }
        }
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

   
