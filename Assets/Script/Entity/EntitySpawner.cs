using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public int EnitiyMaxCount;
    public int EntityCurrentCount;
    public Transform Player;
    public GameObject[] EntityPrefabs;
    public float spawnRadius;

    private void Awake()
    {
        Player = GameManager.Instance.Player.transform;
    }

    private void Start()
    {
        EntityCurrentCount = 0;
        while(EntityCurrentCount < EnitiyMaxCount - 1)
        {
            Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
            int _random = Random.Range(0, EntityPrefabs.Length);
            GameObject _entity = Instantiate(EntityPrefabs[_random], randomPosition, Quaternion.identity);
            Entity _entityScript = _entity.GetComponent<Entity>();
            _entityScript.PlayerTransform = Player;
        }
    }
 


}
