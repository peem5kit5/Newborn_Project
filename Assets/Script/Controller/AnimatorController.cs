using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Transform mainCam;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprite;
    
    private void Start()
    {
        if(!mainCam)
            mainCam = Camera.main.transform;

        if(!anim)
            anim = GetComponent<Animator>();

        if (!sprite)
            sprite = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        BillboardObject();
    }

    private void BillboardObject() => transform.LookAt(mainCam);

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
