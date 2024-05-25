using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
public class ThemeHolder : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private ThemeData[] AllThemeData;

#if UNITY_EDITOR
    private void OnValidate()
    {
        string _folderPath = "Assets/Script/RogueLikeHandlers/Themes";

        if (System.IO.Directory.Exists(_folderPath))
        {
            string[] guids = AssetDatabase.FindAssets("t:ThemeData", new[] { _folderPath });

            AllThemeData = guids.Select(guid => AssetDatabase.LoadAssetAtPath<ThemeData>(AssetDatabase.GUIDToAssetPath(guid))).ToArray();
            Debug.Log($"Found {AllThemeData.Length} ThemeData assets in folder: {_folderPath}");
        }
        else
        {
            Debug.LogError($"The specified folder path does not exist: {_folderPath}");
        }
    }
#endif

    public ThemeData RNG_Theme()
    {
        int _randomTheme = UnityEngine.Random.Range(0, AllThemeData.Length);
        return AllThemeData[_randomTheme];
    }

}
