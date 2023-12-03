using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform Player;

    [Header("Cam Setting")]
    public float RotationSpeed;
    public float ZoomSpeed;
    public float MinZoom;
    public float MaxZoom;

    public float Offset;

    public bool Forced;
    public bool Paused;

    private float currentZoom;
    private float targetZoom;
    private Camera cam; 

    public void PreInit(Camera _cam)
    {
        cam = _cam;
        currentZoom = MaxZoom;
        targetZoom = MaxZoom;
    }
    public void CamRotate(Transform _target)
    {
        float horizontalInput = Input.GetAxis("Mouse X");
        transform.RotateAround(_target.position, Vector3.up, horizontalInput * RotationSpeed * Time.deltaTime);
    }

    public void CamZoom()
    {
        targetZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, MinZoom, MaxZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * 5f);
    }

    public void CamLogic(Transform _target)
    {
        transform.position = new Vector3(_target.position.x, _target.position.y + Offset, _target.position.z);
    }

    public void Update()
    {
        if(cam != null)
        {
            if (!Forced && !Paused)
            {
                CamRotate(Player);
                CamZoom();
                CamLogic(Player);
            }
        }
    }

}

