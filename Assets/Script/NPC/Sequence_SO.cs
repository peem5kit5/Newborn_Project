using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

[CreateAssetMenu(menuName = "Data/Sequence")]
public class Sequence_SO : ScriptableObject
{
    [Header("Time Setting")]
    public float Duration;
    public DayNightManager.DayNight TriggerTime;
    [Space]
    [Header("Target")]
    public Transform Target;
    [Space]
    [Header("Action")]
    public SkeletonAnimation skAnim;
    public Skeleton skeleton;
    public Spine.AnimationState animState;


    public void Init(SkeletonAnimation _skAnim)
    {
        skAnim = _skAnim;
        skeleton = skAnim.Skeleton;
        animState = skAnim.AnimationState;
    }
    public void StartSequence()
    {

    }
}
