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

    public void CamZoom()
    {
        targetZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, MinZoom, MaxZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * 5f);
    }

    public void CamLogic(Transform _target)
    {
        if (Player != null)
            transform.position = Player.position;
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

