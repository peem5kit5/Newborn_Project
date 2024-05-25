using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoarding : MonoBehaviour
{
    [SerializeField] private Transform mainCamPos;

    private void Start()
    {
        if (!mainCamPos)
            mainCamPos = Camera.main.transform;
    }

    private void LateUpdate() => BillboardObject();
    private void BillboardObject() => transform.LookAt(mainCamPos);
}
