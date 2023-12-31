using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TraitManager : Singleton<TraitManager> 
{ 
    public Dictionary<string, Traits_SO> TraitsDictionary = new Dictionary<string, Traits_SO>();

    public Traits_SO[] AllTraits;
    public Dictionary<string, Traits_SO> UnlockedTraits = new Dictionary<string, Traits_SO>();
  
    public override void Awake()
    {
        base.Awake();
        TraitsDictionary = GetAllTraits();
    }
    public Dictionary<string, Traits_SO> GetAllTraits()
    {
        Dictionary<string, Traits_SO> _traitsDict = new Dictionary<string, Traits_SO>();

        foreach (Traits_SO _trait in AllTraits)
            _traitsDict.Add(_trait.TraitName, _trait);

        return _traitsDict;
    }
    public void TryGetTrait(string _traitName)
    {
        if (TraitsDictionary.TryGetValue(_traitName, out var _trait))
            if (_trait.UnlockCondition.CheckCondition())
                UnlockedTraits.Add(_traitName,_trait);
    }
}
