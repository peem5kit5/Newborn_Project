using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRogueLikeHandler : MonoBehaviour
{
    public List<GameObject> Prefabs = new List<GameObject>();
    public ThemeSet_SO Theme;
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
    public virtual void AssignTheme(ThemeSet_SO _theme)
    {

    }

    public virtual void GenerateElements()
    {

    }
    public virtual void CleanUpElements()
    {
        if(Prefabs.Count > 0)
            Prefabs.RemoveRange(0, Prefabs.Count);
    }
}
