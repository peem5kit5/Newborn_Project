using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PoolManager : MonoBehaviour
{
    //RefactoringCode
    public static PoolManager Instance { get; private set; }

    #region Pooling
    public List<Component> ComponentsPoolList;
    public List<Entity> EntityPoolList;
    #endregion


    private void Awake()
    {
        if(Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    public void Spawning(GameObject _gameObject, bool _isSpawn)
    {
        _gameObject.SetActive(_isSpawn);
        Component[] _components = _gameObject.GetComponents<Component>();
        foreach (Behaviour _compo in _components)
        {
            _compo.enabled = _isSpawn;
        }

    }
    public void AddPoolable<t>(Component _t)
    {
        //need to implement
        ComponentsPoolList.Add(_t);
    }

   
    public Entity AddPoolable(Entity _entity)
    {
        if (EntityPoolList.Count > 0)
        {
            foreach(Entity _en in EntityPoolList)
                if(_en == _entity)
                {
                    Spawning(_entity.gameObject, true);
                    return _entity;
                }
        }
        
        EntityPoolList.Add(_entity);

        return _entity;
    }

    public void DespawnPoolable(Behaviour _t)
    {
        if(ComponentsPoolList.Count > 0)
        {
            if (ComponentsPoolList.Contains(_t))
            {
                if (_t is Behaviour)
                {
                    _t.enabled = false;
                }  
            }
        }
    }
    
    public void DespawnPoolable(Entity _entity)
    {
        if (EntityPoolList.Count > 0)
        {
            if (EntityPoolList.Contains(_entity))
            {
                Spawning(_entity.gameObject, false);
            }
        }
    }
}
