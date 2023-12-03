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
    public CameraController CamController;
    [Header("Death Handle")]
    public int DeathCount;
    public Action<int> OnDeathCountChanged;
    public delegate void DeathCounting();
    

    public void Awake()
    {
        PreInit();
    }
    public void Start()
    {
    }
    public void FixedUpdate()
    {
    }
    public void Update()
    {
    }
    public void PreInit()
    {
        Player.PreInit();
        CamController.PreInit(Camera.main);

        OnDeathCountChanged += HandleDeathCount;
    }
    public void Init()
    {
        SetSpawned();

        Cursor.visible = false;
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
    public void DeathCounter()
    {
        DeathCount++;
        OnDeathCountChanged?.Invoke(DeathCount);
    }
    void HandleDeathCount(int _amount)
    {

    }


}
