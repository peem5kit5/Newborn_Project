using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EntityManager : MonoBehaviour
{ 
    public static EntityManager Instance { get; private set; }
    bool isPaused;
    public virtual void InitStart()
    {

    }
    public virtual void InitAwake()
    {

    }
    public virtual void Updating()
    {

    }

    public bool Paused()
    {
        return isPaused;
    }

    public void TogglePaused()
    {
        if (isPaused)
            isPaused = false;
        else
            isPaused = true;
        
    }
    
}
