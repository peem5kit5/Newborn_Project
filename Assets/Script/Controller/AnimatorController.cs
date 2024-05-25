using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BillBoarding))]
public class AnimatorController : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprite;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!anim)
            anim = GetComponent<Animator>();

        if (!sprite)
            sprite = GetComponent<SpriteRenderer>();
    }
#endif

    public void AnimatorControlling(float x, float _magnitude, float z)
    {
        anim.SetFloat("Magnitude", _magnitude);

        if (x > 0f && _magnitude > 0.01f)
            sprite.flipX = true;
        else if (x < 0f && _magnitude > 0.01f)
            sprite.flipX = false;

        if (z > 0f && _magnitude > 0.01f)
            anim.SetBool("Turn", true);
        else if (z < 0f && _magnitude > 0.01f)
            anim.SetBool("Turn", false);
    }
}
