using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineGun : MonoBehaviour
{
    public Transform spineTransform;
    public Transform leftHandTransform;
    public Transform rightHandTransform;
    public float verticalSpeed = 2.0f;
    public float verticalLimit = 45.0f;
    public float horizontalSpeed = 2.0f;
    public float horizontalLimit = 90.0f;

    private float verticalAngle = 0.0f;
    private float horizontalAngle = 0.0f;
    private Quaternion initialSpineRotation;
    private Quaternion initialLeftHandRotation;
    private Quaternion initialRightHandRotation;

 
    // Start is called before the first frame update
    void Start()
    {
        initialSpineRotation = spineTransform.localRotation;
        initialLeftHandRotation = leftHandTransform.localRotation;
        initialRightHandRotation = rightHandTransform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Mouse Y") * -1;
        float horizontalInput = Input.GetAxis("Mouse X") * -1;

        verticalAngle += verticalInput * verticalSpeed;
        verticalAngle = Mathf.Clamp(verticalAngle, -verticalLimit, verticalLimit);

        horizontalAngle += horizontalInput * horizontalSpeed;
        horizontalAngle = Mathf.Clamp(horizontalAngle, -horizontalLimit, horizontalLimit);

        spineTransform.localRotation = initialSpineRotation * Quaternion.Euler(verticalAngle, 0, 0);
        leftHandTransform.localRotation = initialLeftHandRotation * Quaternion.Euler(verticalAngle, 0, 0) * Quaternion.Euler(0, horizontalAngle, 0);
        rightHandTransform.localRotation = initialRightHandRotation * Quaternion.Euler(verticalAngle, 0, 0) * Quaternion.Euler(0, -horizontalAngle, 0);
    }
}
 
