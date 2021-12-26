using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCameraCentroid : MonoBehaviour
{
    private Transform CentralPivot;
    void Start()
    {
        CentralPivot = new GameObject().transform;
        CentralPivot.transform.name = "CentralPivot";
        CentralPivot.transform.position = Vector3.zero;
        gameObject.transform.SetParent(CentralPivot);
        gameObject.transform.localPosition = Vector3.back * 2;
    }
}
