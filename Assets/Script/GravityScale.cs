using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GravityScale : MonoBehaviour
{
    public float GravityPower = 1f;

    public const float globalGravity = -1f;

    Rigidbody rb;

    public bool isGround;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        isGround = true;
    }
    private void Update()
    {
        GravityLogic();
    }
    void GravityLogic()
    {
        if (!isGround)
        {
            Vector3 _gravity = globalGravity * GravityPower * Vector3.up * transform.position.y;
            rb.velocity = _gravity;
        }
    }

    private void OnCollisionStay(Collision _collision)
    {
        if (_collision.collider.CompareTag("Ground"))
            isGround = true;
    }
    private void OnCollisionExit(Collision _collision)
    {
        if (_collision.collider.CompareTag("Ground"))
            isGround = false;   
    }
}
