﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraScr : MonoBehaviour
{
       private float mouseSensitivity = 100f , xRotation = 0f;

    public Transform playerBody;

    private void Awake()
    {  
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation , 0f ,0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
