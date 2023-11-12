using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityScale : MonoBehaviour
{
    public float GravityPower = 1f;

    public const float globalGravity = -10f;

    Rigidbody rb;

    bool isGround;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    private void FixedUpdate()
    {
        GravityLogic();
    }
    void GravityLogic()
    {
        if (!isGround)
        {
            Vector3 _gravity = globalGravity * GravityPower * Vector3.up;
            rb.AddForce(_gravity, ForceMode.Acceleration);
        } 
    }

    private void OnTriggerEnter(Collider _col)
    {
        if (_col.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void OnTriggerExit(Collider _col)
    {
        if (_col.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
