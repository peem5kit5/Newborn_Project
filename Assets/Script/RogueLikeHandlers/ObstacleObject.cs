using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleObject : ObjectBase, IInteractable
{
    public enum ObstacleType
    {
        UnInteractable,
        Interactable
    }

    [Header("Setting")]
    public float CheckRadius;
    public ObstacleType Type;

    [Header("Reference")]
    [SerializeField] private WorldGenerator worldGenerator;
    [SerializeField] private BoxCollider boxCollider;

    public Action<Player> OnInteractable;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!boxCollider)
            boxCollider = GetComponent<BoxCollider>();
        else
            boxCollider.isTrigger = Type == ObstacleType.Interactable;

        if(Type == ObstacleType.Interactable)
        {
            if (gameObject.tag != "InteractableObject")
                gameObject.tag = "InteractableObject";
        }
    }
#endif

    public override void Init(WorldGenerator _worldGen)
    {
        worldGenerator = _worldGen;

        if(Type == ObstacleType.Interactable)
           OnInteractable += Interact;
    }

    public override void DestroyThisObject()
    {
        throw new System.NotImplementedException();
    }

    public bool AllowToSpawn()
    {
        Collider[] _hitColliders = Physics.OverlapSphere(transform.position, CheckRadius);

        foreach (Collider _hitCollider in _hitColliders)
        {
            ObstacleObject _obstacle = _hitCollider.GetComponent<ObstacleObject>();
            if (_obstacle != null && _obstacle != this)
                return false;
        }

        return true;
    }

    public virtual void Interact(Player _player)
    {

    }

    public virtual void OnTriggerEnter(Collider _col)
    {

    }

    public virtual void OnTriggerExit(Collider _col)
    {

    }
}
