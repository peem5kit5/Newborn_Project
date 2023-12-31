using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ThemeHolder : MonoBehaviour
{
    private static ThemeHolder _instance;
    public static ThemeHolder Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Theme is null.");

            return _instance;
        }
    }

    public ThemeSet_SO[] ThemeSets;
    public ThemeSet_SO CurrentThemeSet;

    public int Count;

    public List<ThemeSet_SO> VisitedTheme;

    private void Awake()
    {
        _instance = this;
    }

    public void RollTheme()
    {
        CurrentThemeSet = RNGTheme();
    }

    public ThemeSet_SO RNGTheme()
    {
        List<ThemeSet_SO> _themeList = new List<ThemeSet_SO>();
        for(int i = 0; i < ThemeSets.Length; i++)
        {
            if (ThemeSets[i].Visited) continue;

            _themeList.Add(ThemeSets[i]);
        }

        int _rng = UnityEngine.Random.Range(0, _themeList.Count);
        return _themeList[_rng];
    }

    public void SetTheme(ThemeSet_SO _theme)
    {
        CurrentThemeSet = _theme;
    }

}
