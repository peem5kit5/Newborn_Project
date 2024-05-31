using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectBase : MonoBehaviour
{
    public string ID;
    public int SpawnRate;
    public int IncursionRate;

    public abstract void Init(WorldGenerator _worldGen);

    public abstract void DestroyThisObject();
}
