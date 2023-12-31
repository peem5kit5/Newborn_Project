using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>
{
    public Boss Boss;
    public Player Player;
    public CameraController CamController;

    [Header("Death Handle")]
    public int DeathCount;
    public Action<int> OnDeathCountChanged;
    public delegate void DeathCounting();
    

    public override void Awake()
    {
        base.Awake();
        PreInit();
    }
    public void PreInit()
    {
        Player.Init();
        CamController.Init(Camera.main);

        OnDeathCountChanged += HandleDeathCount;
    }
    public void Init()
    {
        Cursor.visible = false;
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
