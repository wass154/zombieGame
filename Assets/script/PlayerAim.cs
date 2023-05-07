using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform spineTransform;
    public Transform leftHandTransform;
    public Transform rightHandTransform;
    public Transform playerTransform;
    public float verticalSpeed = 2.0f;
    public float MinverticalLimit = 45.0f;
    public float MaxverticalLimit = 45.0f;
    public float horizontalSpeed = 2.0f;
    public float horizontalLimit = 90.0f;

    private float verticalAngle = 0.0f;
    private float horizontalAngle = 0.0f;
    private Quaternion initialSpineRotation;
    private Quaternion initialLeftHandRotation;
    private Quaternion initialRightHandRotation;
    private Quaternion initialPlayerRotation;
    private Vector3 initialPlayerPosition;

    void Start()
    {
        initialSpineRotation = spineTransform.localRotation;
        initialLeftHandRotation = leftHandTransform.localRotation;
        initialRightHandRotation = rightHandTransform.localRotation;
        initialPlayerRotation = playerTransform.localRotation;
        initialPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Mouse Y") * -1;
        float horizontalInput = Input.GetAxis("Mouse X") * -1;

        verticalAngle += verticalInput * verticalSpeed;
        verticalAngle = Mathf.Clamp(verticalAngle, MinverticalLimit, MaxverticalLimit);

        horizontalAngle += horizontalInput * horizontalSpeed;
        horizontalAngle = Mathf.Clamp(horizontalAngle, -horizontalLimit, horizontalLimit);

        spineTransform.localRotation = initialSpineRotation * Quaternion.Euler(verticalAngle, 0, 0);
        leftHandTransform.localRotation = initialLeftHandRotation * Quaternion.Euler(verticalAngle, 0, 0) * Quaternion.Euler(0, horizontalAngle, 0);
        rightHandTransform.localRotation = initialRightHandRotation * Quaternion.Euler(verticalAngle, 0, 0) * Quaternion.Euler(0, -horizontalAngle, 0);

        // Rotate the player's body based on the horizontal mouse input
        playerTransform.localRotation = initialPlayerRotation * Quaternion.Euler(0, horizontalAngle, 0);

        // Move the player's body forward/backward based on vertical mouse input
        float forwardMovement = Mathf.Cos(verticalAngle * Mathf.Deg2Rad) * verticalInput;
        playerTransform.position = initialPlayerPosition + (playerTransform.forward * forwardMovement);
    }
}
