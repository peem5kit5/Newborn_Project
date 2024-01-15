using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class ThemeHolder : MonoBehaviour
{
    public RuleTheme[] AllThemes;
    public RuleTheme CurrentTheme;

    private Dictionary<string, RuleTheme> themesTable = new Dictionary<string, RuleTheme>();

    public void Init()
    {
        themesTable = GetData();
        RNGThemeAndGetTheme();
    }

    private Dictionary<string, RuleTheme> GetData()
    {
        Dictionary<string, RuleTheme> _themeDict = new Dictionary<string, RuleTheme>();

        foreach (RuleTheme _ruleTheme in AllThemes)
            _themeDict.Add(_ruleTheme.ThemeKey, _ruleTheme);

        return _themeDict;
    }

    public void RNGThemeAndGetTheme()
    {
        System.Random _random = new System.Random();
        int _randomIndex = _random.Next(themesTable.Count);

        KeyValuePair<string, RuleTheme> _randomPair = themesTable.ElementAt(_randomIndex);

        CurrentTheme = _randomPair.Value;
        DungeonGenerator.Instance.SetUpDungeon(CurrentTheme);
    }

    public void NextStageOfTheme()
    {

    }
}
