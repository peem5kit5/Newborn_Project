using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform Player;

    [Header("Cam Setting")]
    public float ZoomSpeed;
    public float MinZoom;
    public float MaxZoom;

    public float Offset;

    public bool Forced;
    public bool Paused;

    private float targetZoom;
    private Camera cam; 

    public void Init(Camera _cam)
    {
        cam = _cam;
        targetZoom = MaxZoom;
        Cursor.visible = false;
    }

    //public void CamRotate(Transform _target)
    //{
    //    if (Input.GetMouseButton(1))
    //    {
    //        float _horizontalInput = Input.GetAxis("Mouse X");
    //        transform.RotateAround(_target.position, Vector3.up, _horizontalInput * RotationSpeed * Time.deltaTime);
    //    }
    //}

    public void CamZoom()
    {
        targetZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, MinZoom, MaxZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * 5f);
    }

    public void CamLogic(Transform _target)
    {
        if (Player != null)
        {
            //Vector3 newPosition = new Vector3(Player.position.x, Player.position.y + Offset, Player.position.z - Offset);
            transform.position = Player.position;

            //transform.LookAt(Player);
        }
    }

    public void Update()
    {
        if(cam != null)
        {
            if (!Forced && !Paused)
            {
                CamZoom();
                CamLogic(Player);
            }
        }
    }

}

