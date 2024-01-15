using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    public GameObject[] walls; // 0 - Up 1 -Down 2 - Right 3- Left
    public void UpdateRoom(bool[] status)
    {
        if(walls.Length > 0)
        for (int i = 0; i < status.Length; i++)
            walls[i].SetActive(!status[i]);
    }
}
