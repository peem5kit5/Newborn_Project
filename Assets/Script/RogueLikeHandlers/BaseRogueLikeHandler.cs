using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRogueLikeHandler : MonoBehaviour
{
    public void Awake()
    {
        PreInit();
    }
    public void Start()
    {
        Init();
    }
    public virtual void PreInit()
    {

    }
    public virtual void Init()
    {

    }
    public virtual void AssignTheme()
    {

    }

    public virtual void GenerateElements()
    {

    }
    public virtual void CleanUpElements()
    {
    }
}
