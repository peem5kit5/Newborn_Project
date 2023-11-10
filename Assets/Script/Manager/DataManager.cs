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

    public const string NewGameScene = "New Game";

    public static DataManager Instance { get; private set; }


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
                if(i == 0)
                {
                    SaveSlot[0].onClick.AddListener(() =>
                    {
                        Load(0);
                    });
                }
                else if(i == 1)
                {
                    SaveSlot[1].onClick.AddListener(() =>
                    {
                        Load(1);
                    });
                }
                else if(i == 2)
                {
                    SaveSlot[2].onClick.AddListener(() =>
                    {
                        Load(2);
                    });
                }
               
            }
            else
            {
                if (i == 0)
                {
                    SaveSlot[0].onClick.AddListener(() =>
                    {
                        NewGame(0);
                    });
                }
                else if (i == 1)
                {
                    SaveSlot[1].onClick.AddListener(() =>
                    {
                        NewGame(1);
                    });
                }
                else if (i == 2)
                {
                    SaveSlot[2].onClick.AddListener(() =>
                    {
                        NewGame(2);
                    });
                }
            }
        }
               
        
    }
    public void NewGame(int _slot)
    {
        string _path = Application.dataPath + _slot + "/savedata.json";
        if (File.Exists(_path))
        {
            File.Delete(_path);
            SceneManager.LoadScene("New Game");
        }
    }
    public void AddData(string _id, float _data)
    {
        FloatDataHolders.Add(_id, _data);
    }
    public void AddData(string _id, int _data)
    {
        FloatDataHolders.Add(_id, _data);
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
        foreach (string _key in FloatDataHolders.Keys)
        {
            if (_key == _id)
            {
                _data = (int)FloatDataHolders[_key];
            }
        }
        return _data;
    }
    public float LoadData(string _id, float _data)
    {
        foreach(string _key in FloatDataHolders.Keys)
        {
            if(_key == _id)
            {
                _data = FloatDataHolders[_key];
            }
        }
        return _data;
    }
    public string LoadData(string _id, string _data)
    {
        foreach (string _key in StringDataHolders.Keys)
        {
            if (_key == _id)
            {
                _data = StringDataHolders[_key];
            }
        }
        return _data;
    }
    public void Save(JSONNode _node)
    {
       

        foreach(string _string in FloatDataHolders.Keys)
        {
            float _float = FloatDataHolders[_string];
            _node.Add(_string, _float);
        }

        foreach (string _string in StringDataHolders.Keys)
        {
            string _str = StringDataHolders[_string];
            _node.Add(_string, _str);
        }

        File.WriteAllText(Application.dataPath + _node["Slot"].AsInt.ToString() + "/savedata.json", _node);
    }
    public void Load(int _slot)
    {
        string _path = GetFilePath(_slot);
        if (File.Exists(_path))
        {
            string _json = File.ReadAllText(_path);
            JSONNode _node = JSONNode.Parse(_json);

            foreach(string _key in _node.Keys)
            {
                if (_node[_key].IsNumber)
                {
                    FloatDataHolders.Add(_key, _node[_key].AsFloat);
                    continue;
                }

                StringDataHolders.Add(_key, _node[_key]);

                
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
        return Path.Combine(Application.dataPath + _slot + "/savedata.json");
    }
    public bool GetData()
    {
        return false;
    }
}


