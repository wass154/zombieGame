using UnityEngine;

public class Aim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _spine;
    [SerializeField] private Transform _spine1;
    [SerializeField] private Transform _spine2;

    [Header("Settings")]
    [SerializeField] private float _verticalSpeed = 2f;
    [SerializeField] private float _horizontalSpeed = 2f;
    [SerializeField] private float _maxVerticalAngle = 45f;
    [SerializeField] private float _minVerticalAngle = -45f;
    [SerializeField] private float _maxHorizontalAngle = 45f;
    [SerializeField] private float _minHorizontalAngle = -45f;

    private float _verticalRotation = 0f;
    private float _horizontalRotation = 0f;

    void Update()
    {
        if (PickUp.isUse)
        {
            A();
        }
    }
    void A()
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
    }
}