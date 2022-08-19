using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenBorder : MonoBehaviour
{
    [SerializeField] private static float _height;
    [SerializeField] private static float _width;
    private float _cameraSize;

    public static float Height
    {
        get => _height;
    }

    public static float Width
    {
        get => _width;
    }

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        float aspect = (float)Screen.width / Screen.height;

        _height = _mainCamera.orthographicSize;
        _width = (_height * aspect);
    }
}