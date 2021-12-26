using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures;
using UnityEngine.UI;
public class FlickCameraControl : MonoBehaviour
{
    [SerializeField] private Button LeftButton;
    [SerializeField] private Button RightButton;
    [SerializeField] private Button UpButton;
    [SerializeField] private Button DownButton;
    [SerializeField] private Transform CameraTransform;
    public Vector3 vectorRotation;
    private void OnDown()
    {
        Vector3 rotationVector = new Vector3(0, 90, 0);
        Quaternion rotation = Quaternion.Euler(rotationVector);
        CameraTransform.rotation = rotation;
    }
    private void OnUp()
    {
       
    }

    private void OnLeft()
    {

    }
    private void OnRight()
    {

    }

    private void OnHorizontalGestura(object obj, System.EventArgs args)
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            OnDown();


        transform.RotateAround(CameraTransform.position, vectorRotation, 20 * Time.deltaTime);
    }
}
