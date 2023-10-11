using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player Player;

    [Header("Death Handle")]
    public int DeathCount;
    public Action<int> OnDeathCountChanged;
    public delegate void DeathCounting();

    public void Awake()
    {
        InitAwake();
    }
    public void Start()
    {
        InitStart();
    }
    public void FixedUpdate()
    {
        FixedUpdating();
    }
    public void Update()
    {
        Updating();
    }
    public void Init()
    {
        
    }
    void SetUp()
    {
        Player.Init();
       // Inventory.Instance.Init();
        OnDeathCountChanged += HandleDeathCount;
        SetSpawned();
    }
    void SetSpawned()
    {
        GameObject[] _entityOBJ = GameObject.FindGameObjectsWithTag("Spawner");
        
        if(_entityOBJ.Length > 0)
        {
            foreach(GameObject _obj in _entityOBJ)
            {
                EntitySpawner _spawners = _obj.GetComponent<EntitySpawner>();
                _spawners.SpawnEnemy();
            }
        }    
        
    }
    public virtual void InitAwake()
    {
        SetUp();
    }
    public virtual void InitStart()
    {

    }
    public virtual void Updating()
    {

    }
    public virtual void FixedUpdating()
    {

    }

    public void DeathCounter()
    {
        DeathCount++;
        OnDeathCountChanged?.Invoke(DeathCount);
    }

    void HandleDeathCount(int _amount)
    {

    }


}
