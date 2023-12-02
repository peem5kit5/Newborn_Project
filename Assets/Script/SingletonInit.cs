using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonInit : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.Init();
    }
}
