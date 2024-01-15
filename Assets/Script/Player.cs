using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;


public class Player : MonoBehaviour
{
    public IInteractable Interact { get; set; }

    Rigidbody rb;
    [Header("Attributes")]
    public float MoveSpeed = 0.5f;
    public float BaseMaxSpeed;
    public float CurrentJump = 0;
    public float RotationSpeed = 5;
    public void Init()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        MoveLogic();
        CheckInteract();
        //CheckGuild(Guild);
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Stat.Increased("Agi", 10);
        //}
    }
    void MoveLogic()
    {
        float _horizontalInput = Input.GetAxisRaw("Horizontal");
        float _verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 _cameraForward = Camera.main.transform.forward;
        _cameraForward.y = 0f;
        _cameraForward.Normalize();

        Vector3 _input = _cameraForward * _verticalInput + Camera.main.transform.right * _horizontalInput;

        if (_input != Vector3.zero)
        {
            Quaternion _targetRotation = Quaternion.LookRotation(_input.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * RotationSpeed);
        }

        rb.velocity = _input.normalized * MoveSpeed;
    }
   
    void CheckInteract()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact?.Interact(this);
        }
    }
}
