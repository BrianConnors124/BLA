using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoomOut : MonoBehaviour
{
    private float origin;

    public CinemachineVirtualCamera newCamera;
    
    
    private void Start()
    {
        origin = newCamera.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        for (int i = 0; i < 100; i++)
        {
            print("run " + i);
            InputSystemController.instance._zoomIn += ZoomIn;
            InputSystemController.instance._zoomOut += ZoomOut;
        }
    }

    private void ZoomIn()
    {
        print("ZoomIn");
        newCamera.m_Lens.OrthographicSize = origin;
    }
    private void ZoomOut()
    {
        print("ZoomOut");
        newCamera.m_Lens.OrthographicSize += 3;
    }
}
