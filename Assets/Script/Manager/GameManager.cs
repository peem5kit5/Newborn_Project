using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject Player;


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

    void SetUp()
    {
        OnDeathCountChanged += HandleDeathCount;
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
