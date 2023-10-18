using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class SkinChanger : MonoBehaviour
{
    public static SkinChanger Instance { get; private set; }
    SkeletonAnimation skAnim;
    Skeleton skeleton;
    Spine.AnimationState animState;
    private void Awake()
    {
        SetUp();
    }

    void SetUp()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        skAnim = GetComponent<SkeletonAnimation>();
        skeleton = skAnim.Skeleton;

        animState = skAnim.AnimationState;
        
    }

    public void Equip(string _part, string _skinName)
    {

        var _slot = skAnim.Skeleton.FindSlot(_part);

        _slot.Attachment = skAnim.Skeleton.GetAttachment(_part, _skinName);
    }

    public void UnEquip(string _part, string _skinName)
    {
        var _slot = skAnim.Skeleton.FindSlot(_part);

        _slot.Attachment = null;
    }



}
