using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public UI_Manager Instance { get; private set; }

    #region UI
    public UI_Inventory Ui_Inventory;
    #endregion
    public void Awake()
    {
        Init();
    }
    public void Init()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


}
