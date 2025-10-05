using System;
using UnityEngine;

public class ControleCameras : MonoBehaviour
{
    public Camera[] cameras = new Camera[5];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // controle cameras
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActiverCamera(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActiverCamera(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActiverCamera(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActiverCamera(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ActiverCamera(4);
        }
    }

    void ActiverCamera(int index)
    {
        foreach (Camera cam in cameras)
        {
            cam.enabled = false;
        }
        cameras[index].enabled = true;
    }
}
