using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class IncursionMaker : MonoBehaviour
{
    [SerializeField] private TilePattern currentTile;
    [SerializeField] private WorldGenerator worldGenerator;

    public float Range;

    private void OnDrawGizmos() => Gizmos.DrawSphere(this.transform.position, Range);


    //public void CheckOverlapToIncursion(TilePattern _tilePattern, WorldGenerator _worldGen)
    //{
    //    if (!worldGenerator)
    //        worldGenerator = _worldGen;

    //    currentTile = _tilePattern;

    //    Collider[] _hitColliders = Physics.OverlapSphere(gameObject.transform.position, Range);

    //    foreach(var _hitCollider in _hitColliders)
    //    {
    //        var _tile = _hitCollider.GetComponent<TilePattern>();

    //        if(_tile != null)
    //        _tile.SetTileOverride(currentTile);
    //    }
    //}

    //public void SpawnIncursion(Vector3 _centerPosition)
    //{
    //    Vector3 _currentTilePos = worldGenerator.GetTilePosAndOffset();
        
    //    Vector3[] _positions = new Vector3[]
    //    {
    //        _centerPosition,                               // Center
    //        _centerPosition + new Vector3(_currentTilePos.x, 0, 0), // Right
    //        _centerPosition + new Vector3(-_currentTilePos.x, 0, 0),// Left
    //        _centerPosition + new Vector3(0, 0, _currentTilePos.z), // Forward
    //        _centerPosition + new Vector3(0, 0, -_currentTilePos.z) // Backward
    //    };

    //    foreach (Vector3 _position in _positions)
    //        Instantiate(currentTile.gameObject, _position, Quaternion.identity);
    //}
}
