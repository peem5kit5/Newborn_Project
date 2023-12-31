using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    [Header("Data Setting")]
    public ThemeSet_SO EntityTheme;

    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private NavMeshAgent agent;
}
