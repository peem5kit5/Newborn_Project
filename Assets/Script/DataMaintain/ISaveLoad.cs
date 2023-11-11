using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
public interface ISaveLoad
{
    public void Save(JSONNode _node);

    public void Load(int _slot);
}
