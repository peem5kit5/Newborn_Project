using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    public Transform Target;
    public Vector3 offset;
    public const float followSpeed = 5f;
    public float distance = 5f;

    private int index = 0;
    void Start()
    {
        SetUp();
    }
    void SetUp()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        float _angleRadians = 45 * Mathf.Deg2Rad;
        float _xOffset = Mathf.Sin(_angleRadians) * distance;
        float _zOffset = -Mathf.Cos(_angleRadians) * distance;
        offset = new Vector3(_xOffset, 0, _zOffset);
    }
    void FixedUpdate()
    {
        
    }
    private void Update()
    {
        CameraLogic();
    }
    public void ChangeTargetToFocus(Transform _target)
    {
        Target = _target;
    }
    void ChangeIndex()
    {

    }
    public void CameraLogic()
    {
        if (Target != null)
        { 
            Vector3 _newPosition = new Vector3(Target.position.x, transform.position.y, Target.position.z);
            transform.position = _newPosition + offset;

            transform.LookAt(Target);
            if (Input.GetKeyDown(KeyCode.Q))
                index = (index - 1 + 8) % 8;
            else if (Input.GetKeyDown(KeyCode.E))
                index = (index + 1) % 8;


            float _targetRotation = index * 45f;
            transform.RotateAround(Target.position, Vector3.up, _targetRotation - transform.rotation.eulerAngles.y);

        }
    }
}

