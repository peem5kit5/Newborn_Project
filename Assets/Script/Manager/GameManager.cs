using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] private DungeonGenerator dungeonGenerator;

    public Transform Boss;
    public Player Player;
    public int Difference;

    public CameraController CamController;
    public ThemeHolder ThemeHolder;

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
        ThemeHolder.Init();
        //DungeonGenerator.Instance.CalcuateHowFarFromBoss(Player.transform, Boss, Difference);
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
