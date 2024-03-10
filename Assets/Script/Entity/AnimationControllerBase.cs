using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerBase : MonoBehaviour
{
    public List<AnimationClip> Animations;

    private Animator anim;
    private Dictionary<string, AnimationClip> animationDict = new Dictionary<string, AnimationClip>();

    public void Init()
    {
        anim = GetComponent<Animator>();

        if(Animations.Count > 0)
        foreach(AnimationClip _anim in Animations)
        {
                Debug.Log(_anim.name);
        }
    }

    private void Start()
    {
        
    }
}
