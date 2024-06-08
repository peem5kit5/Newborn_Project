using System.Collections.Generic;
using Unity.VisualScripting;
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
    public string Direction;
    [SerializeField] private bool Settled;

    public List<TilePattern> _testList = new List<TilePattern>();
    private Dictionary<int, SurroundTileData> _surroundTileData = new Dictionary<int, SurroundTileData>();

    private void OnEnable()
    {
        if (!sprite)
            sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private Vector3[] TileDirection()
    {
        Vector3[] _directions = {
            Vector3.forward.normalized,
            Vector3.back.normalized,
            Vector3.right.normalized,
            Vector3.left.normalized,
            (Vector3.forward + Vector3.right).normalized,
            (Vector3.forward + Vector3.left).normalized,
            (Vector3.back + Vector3.right).normalized,
            (Vector3.back + Vector3.left).normalized
        };

        return _directions;
    }

    public void SetSprite(Sprite _sprite)
    {
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
        Settled = true;
    }

    public void OverrideTile(TilePattern _tilePattern ,bool _isOverride = true)
    {
        TileData = _tilePattern.TileData;
        ID = _tilePattern.ID;
        gameObject.name = "OverridedTile_" + _tilePattern.ID;

        sprite.sprite = _tilePattern.Sprite.sprite;
        walkable = _tilePattern.Walkable;
        surroundRange = _tilePattern.SurroundRange;

        IncursionRate = _tilePattern.IncursionRate;

        _tilePattern.SetSurround();
    }

    public void SetTileBiome(int _biomeCount)
    {
        if (!BiomeChecker())
        {
            SetSurround();
            return;
        }

        int _index = _biomeCount;
        for (int i = 0; i < TileDirection().Length; i++)
        {
            if (_index <= 0) break;

            Collider[] _hits = Physics.OverlapSphere(transform.position, _biomeCount);
            foreach (var _hit in _hits)
            {
                var _tileScript = _hit.transform.GetComponent<TilePattern>();
                if (_tileScript != null)
                {
                    _testList.Add(_tileScript);
                    _tileScript.OverrideTile(this);
                    _tileScript.SetSurround();
                    _index--;
                }
            }
        }
    }

    private bool BiomeChecker()
    {
        if (TileData.SurroundDatas.Length <= 0) 
            return false;

        return Random.Range(0, 100) <= TileData.BiomeChance;
    }

    private void SetSurround()
    {
        if (TileData.SurroundDatas.Length <= 0) return;
        if (isOverride) return;

        isOverride = true;

        for (int i = 0; i < TileData.SurroundDatas.Length; i++)
            _surroundTileData.Add((int)TileData.SurroundDatas[i].SetDirection, TileData.SurroundDatas[i]);
  
        for (int i = 0; i < TileDirection().Length; i++)
        {
            if (_surroundTileData.TryGetValue(i, out var _sur))
                SurroundSetting(TileDirection()[i], _sur);
            else
                i--;
        }
    }

    private void SurroundSetting(Vector3 _direction, SurroundTileData _sur)
    {
        RaycastHit[] _hits = Physics.RaycastAll(transform.position, _direction, surroundRange);
        foreach (var _hit in _hits)
        {
            var _tileScript = _hit.transform.GetComponent<TilePattern>();

            if (_tileScript == null) continue;

            if (_tileScript.ID == ID) continue;

            if (_tileScript.Direction != "")
            {
                ValidateTile(_direction, _tileScript, _sur);
                continue;
            }   

            _tileScript.Direction = GetSurroundID(_direction);
            _tileScript.SetSprite(_sur.Sprite);
        }
    }

    private string GetSurroundID(Vector3 _dir)
    {
        if (_dir == Vector3.forward.normalized)
            return "Up";

        if (_dir == Vector3.back.normalized)
            return "Down";

        if (_dir == Vector3.right.normalized)
            return "Right";

        if (_dir == Vector3.left.normalized)
            return "Left";

        if (_dir == (Vector3.forward + Vector3.right).normalized)
            return "Up_Right";

        if (_dir == (Vector3.forward + Vector3.left).normalized)
            return "Up_Left";

        if (_dir == (Vector3.back + Vector3.right).normalized)
            return "Down_Right";

        if (_dir == (Vector3.back + Vector3.left).normalized)
            return "Down_Left";

        return "";
    }

    private void ValidateTile(Vector3 _dir, TilePattern _tileScript, SurroundTileData _sur)
    {
        Dictionary<CorrectTileData.Direction, CorrectTileData> _correctTileDict = new Dictionary<CorrectTileData.Direction, CorrectTileData>();

        if (_sur.CorrectTileDatas.Length > 0)
        {
            foreach (var _correctTileData in _sur.CorrectTileDatas)
                _correctTileDict.Add(_correctTileData.CorrectTileDirection, _correctTileData);
        }
        else
            return;

        string _surroundID = GetSurroundID(_dir);

        switch (_tileScript.Direction)
        {
            case "Up":
                if (_dir != Vector3.forward.normalized)
                {
                    if (_surroundID == "Up_Right")
                        _tileScript.SetSprite(_correctTileDict[CorrectTileData.Direction.Right].CorrectSprite);
                    if (_surroundID == "Up_Left")
                        _tileScript.SetSprite(_correctTileDict[CorrectTileData.Direction.Left].CorrectSprite);
                }
                break;
            case "Down":
                if (_dir != Vector3.back.normalized)
                {
                    if (_surroundID == "Down_Right")
                        _tileScript.SetSprite(_correctTileDict[CorrectTileData.Direction.Right].CorrectSprite);
                    if (_surroundID == "Down_Left")
                        _tileScript.SetSprite(_correctTileDict[CorrectTileData.Direction.Left].CorrectSprite);
                }
                break;
            case "Right":
                if (_dir != Vector3.right.normalized)
                {
                    if (_surroundID == "Up_Right")
                        _tileScript.SetSprite(_correctTileDict[CorrectTileData.Direction.Up].CorrectSprite);
                    if (_surroundID == "Down_Right")
                        _tileScript.SetSprite(_correctTileDict[CorrectTileData.Direction.Down].CorrectSprite);
                }
                break;
            case "Left":
                if (_dir != Vector3.left.normalized)
                {
                    if (_surroundID == "Up_Left")
                        _tileScript.SetSprite(_correctTileDict[CorrectTileData.Direction.Up].CorrectSprite);
                    if (_surroundID == "Down_Left")
                        _tileScript.SetSprite(_correctTileDict[CorrectTileData.Direction.Down].CorrectSprite);
                }
                break;
            case "Up_Right":
                if (_dir != (Vector3.forward + Vector3.right).normalized)
                {

                }
                break;
            case "Up_Left":
                if (_dir != (Vector3.forward + Vector3.left).normalized)
                {

                }
                break;
            case "Down_Right":
                if (_dir != (Vector3.back + Vector3.right).normalized)
                {

                }
                break;
            case "Down_Left":
                if (_dir != (Vector3.back + Vector3.left).normalized)
                {

                }
                break;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, surroundRange);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.forward.normalized * surroundRange); // North
        Gizmos.DrawRay(transform.position, Vector3.back.normalized * surroundRange);    // South
        Gizmos.DrawRay(transform.position, Vector3.right.normalized * surroundRange);   // East
        Gizmos.DrawRay(transform.position, Vector3.left.normalized * surroundRange);    // West
        Gizmos.DrawRay(transform.position, (Vector3.forward + Vector3.right).normalized * surroundRange); // North-East
        Gizmos.DrawRay(transform.position, (Vector3.forward + Vector3.left).normalized * surroundRange);  // North-West
        Gizmos.DrawRay(transform.position, (Vector3.back + Vector3.right).normalized * surroundRange);    // South-East
        Gizmos.DrawRay(transform.position, (Vector3.back + Vector3.left).normalized * surroundRange);     // South-West
    }
#endif

    public override void Init(WorldGenerator _worldGen)
    {
        worldGen = _worldGen;
    }

    public override void DestroyThisObject()
    {
        throw new System.NotImplementedException();
    }
}

   
