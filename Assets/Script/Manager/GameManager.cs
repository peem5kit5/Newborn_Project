using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>
{
    [Header("Property")]
    public Transform Boss;
    public Player Player;
    public int Difficulty;

    [Space]
    [Header("Theme (Need To Play First)")]
    public ThemeData CurrentThemeData;

    [Space]
    [Header("Reference")]
    [SerializeField] private WorldGenerator worldGenerator;
    [SerializeField] private ThemeHolder themeHolder;
    [SerializeField] private CameraController CamController;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!Player)
            Player = FindObjectOfType<Player>();

        if (!CamController)
            CamController = FindObjectOfType<CameraController>();

        if (!worldGenerator)
        {
            worldGenerator = FindObjectOfType<WorldGenerator>();
            
            if (worldGenerator) 
                return;
            else 
                Debug.LogError("There no World Generator.");
        }

        if (!themeHolder)
        {
            themeHolder = FindObjectOfType<ThemeHolder>();

            if (themeHolder)
                return;
            else
                Debug.LogError("There no World Generator.");
        }
    }
#endif

    public override void Awake()
    {
        base.Awake();
        PreInit();
    }

    public override void Start()
    {
        Init();
    }

    public void PreInit()
    {
        Player.Init();
        CamController.Init(Camera.main);

        CurrentThemeData = themeHolder.RNG_Theme();
        worldGenerator.Init(CurrentThemeData);

        //Need Implement
        //ThemeHolder.Init();
        //DungeonGenerator.Instance.CalcuateHowFarFromBoss(Player.transform, Boss, 10);
    }
    public void Init()
    {
        Cursor.visible = false;
    }
    public void DeathCounter()
    {

    }
    void HandleDeathCount(int _amount)
    {

    }
}
