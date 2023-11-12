using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public IInteractable Interact { get; set; }

    [Header("Setting")]
    public Guild_SO Guild;
    Rigidbody rb;

    [Header("Attributes")]
    public RPG_Stats Stat;
    public float MoveSpeed = 0.5f;
    public float BaseMaxSpeed;
    public float MaxSpeed;
    public float CurrentJump = 0;
    public void Init()
    {
     
        rb = GetComponent<Rigidbody>();
       
        Stat.AgiChanged += UpdateMoveSpeed;
        Stat.AgiChanged += UpdateAttackSpeed;
        Stat.Increased("Agi", 10);

    }
    public void Update()
    {
        MoveLogic();
        CheckInteract();
        CheckGuild(Guild);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Stat.Increased("Agi", 10);
        //}
        
    }
    public void CheckGuild(Guild_SO _guild)
    {
        if(_guild != null)
        {
            
        }
    }
    public void GuildChanger(Guild_SO _guild)
    {
        Guild = _guild;
    }
    public Guild_SO GetGuild()
    {
        return Guild;
    }
    void MoveLogic()
    {
        float _horizontalInput = Input.GetAxisRaw("Horizontal");
        float _verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 _input = cameraForward * _verticalInput + Camera.main.transform.right * _horizontalInput;

        rb.velocity = _input.normalized * MoveSpeed;

        bool isIncreasedSpeed = rb.velocity.magnitude > 0.1;

        if (isIncreasedSpeed)
        {
            if (MoveSpeed < MaxSpeed)
            {
                MoveSpeed = Mathf.MoveTowards(MoveSpeed, MaxSpeed, Stat.Agi * Time.deltaTime / 2);
            }
            else
            {
                MoveSpeed = MaxSpeed;
            }
        }
        else
        {
            MoveSpeed = 0.5f;
        }
    }
   
    void CheckInteract()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact?.Interact(this);
            Debug.Log("Chat");
        }
    }
    void UpdateMoveSpeed(int _agi)
    {
        MaxSpeed = _agi * 0.5f;
    }
    void UpdateAttackSpeed(int _agi)
    {

    }
    void UpdateBaseDamage(int _amount)
    {

    }
   
}
