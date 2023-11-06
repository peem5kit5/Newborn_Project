using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;
public class DataManager : MonoBehaviour, ISaveLoad
{
    public static DataManager Instance { get; private set; }

    public Dictionary<string, int> IntDataHolders;

    public Dictionary<string, float> FloatDataHolders;

    public Dictionary<string, string> StringDataHolders;

    public Button[] SaveSlot;
    private void Awake()
    {
        SetUp();
    }

    void SetUp()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void SetButton()
    {
        
        for (int i = 0; i < SaveSlot.Length; i++)
        {
            SaveSlot[i].gameObject.SetActive(true);
            if (File.Exists(GetFilePath(i)))
            {
                SaveSlot[i].onClick.AddListener(() =>
                {
                    //LoadSaveData(i);
                });
            }
            else
            {
                SaveSlot[i].onClick.AddListener(() =>
                {
                   // NewGame(i);
                });
            }
        }
               
        
    }

    public void AddData(string _id, float _data)
    {
        FloatDataHolders.Add(_id, _data);
    }
    public void AddData(string _id, int _data)
    {
        IntDataHolders.Add(_id, _data);
    }
    public void AddData(string _id, string _data)
    {
        StringDataHolders.Add(_id, _data);
    }

    #region SaveSlots
    public void Save(int _slot)
    {
        JSONNode _node = new JSONObject();
        _node.Add("Slot", _slot);

        Save(_node);
    }
    public int LoadData(string _id, int _data)
    {

        return _data;
    }
    public float LoadData(string _id, float _data)
    {

        return _data;
    }
    public string LoadData(string _id, string _data)
    {

        return _data;
    }
    public void Save(JSONNode _node)
    {
        foreach(string _string in IntDataHolders.Keys)
        {
            int _int = IntDataHolders[_string];
            _node.Add(_string, _int);
        }

        foreach(string _string in FloatDataHolders.Keys)
        {
            float _int = FloatDataHolders[_string];
            _node.Add(_string, _int);
        }

        foreach (string _string in StringDataHolders.Keys)
        {
            string _int = StringDataHolders[_string];
            _node.Add(_string, _int);
        }

        File.WriteAllText(Application.dataPath + _node["Slot"].AsInt.ToString() + "/savedata.json", _node);
    }
    public void Load(int _slot)
    {
        string _path = Application.dataPath + _slot + "/savedata.json";
        if (File.Exists(_path))
        {
            string _json = File.ReadAllText(_path);
            JSONNode _node = JSONNode.Parse(_json);

            foreach(string _key in _node.Keys)
            {
                //Implement This
            }
            
        }
    }



    #endregion

    #region LoadSlots



    public void LoadGame()
    {
        
    }

    private IEnumerator Load()
    {
        yield return new WaitForSeconds(1);
    }
  
    
    #endregion
    public static string GetFilePath(int _slot)
    {
        return Path.Combine(Application.persistentDataPath, "save/" + _slot + ".json");
    }
    public bool GetData()
    {
        return false;
    }
}


