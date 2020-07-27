using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor;
    public float mouseSensitivity = 10;
    public Transform target; // player
    public float distancefromTarget = 2; // distance from target
    float yaw; // Y axis
    float pitch; // X axis

    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmmothVelocity;
    Vector3 currentRotation;
    void Start()
    {
        //locks cursor...(duh)
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        // Basic Input
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        // clamping the camera on the x axis
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        //Rotation(smooths the rotation aswell)
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmmothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;
        //
        transform.position = target.position - transform.forward * distancefromTarget;
    }

    
}
