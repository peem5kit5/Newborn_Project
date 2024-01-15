using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityTest : MonoBehaviour
{
    private NavMeshAgent nav;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
      
    }

    private void Update()
    {
        nav.SetDestination(GameManager.Instance.Player.transform.position);
    }
}
