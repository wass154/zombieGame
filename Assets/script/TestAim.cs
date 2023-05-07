using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _spine;
    [SerializeField] private Transform _spine1;
    [SerializeField] private Transform _spine2;
    [SerializeField] private Transform _crosshair;

    [Header("Settings")]
    [SerializeField] private float _verticalSpeed = 2f;
    [SerializeField] private float _horizontalSpeed = 2f;
    [SerializeField] private float _maxVerticalAngle = 45f;
    [SerializeField] private float _minVerticalAngle = -45f;
    [SerializeField] private float _maxHorizontalAngle = 45f;
    [SerializeField] private float _minHorizontalAngle = -45f;

    private float _verticalRotation = 0f;
    private float _horizontalRotation = 0f;
    private bool _isAiming = false;
    private Vector3 _initialCrosshairPosition;

    void Start()
    {
        _initialCrosshairPosition = _crosshair.position;
    }

    void Update()
    {
        if (PickUp.isUse)
        {
            TestAiming();
        }
        if (Switcher.isT)
        {
            _maxHorizontalAngle = 0;
            _minHorizontalAngle= 0;
        }
        else
        {
            _maxHorizontalAngle = 40f;
            _minHorizontalAngle = -40f;
        }
    }
    void TestAiming()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _isAiming = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _isAiming = false;
        }

        if (_isAiming)
        {
            float mouseX = Input.GetAxis("Mouse X") * _horizontalSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * _verticalSpeed;

            _horizontalRotation += mouseX;
            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, _minVerticalAngle, _maxVerticalAngle);
            _horizontalRotation = Mathf.Clamp(_horizontalRotation, _minHorizontalAngle, _maxHorizontalAngle);

            _spine.localRotation = Quaternion.Euler(_verticalRotation, _horizontalRotation, 0f);
            _spine1.localRotation = Quaternion.Euler(_verticalRotation, _horizontalRotation, 0f);
            _spine2.localRotation = Quaternion.Euler(_verticalRotation, _horizontalRotation, 0f);

            Vector3 crosshairPosition = _initialCrosshairPosition;
            crosshairPosition.x += mouseX * Time.deltaTime;
            crosshairPosition.y -= mouseY * Time.deltaTime;
            _crosshair.position = crosshairPosition;
        }
    }
}