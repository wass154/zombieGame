using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCam : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public Transform cameraTransform;
    public float mouseSensitivity = 100f;

    private float horizontalInput;
    private float verticalInput;
    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;

    [SerializeField] float Max, Min;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
       
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, Min, Max);
 //cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
       
      
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            animator.SetFloat("inputX", horizontalInput);
            animator.SetFloat("inputY", verticalInput);

            // Calculate the direction to move based on camera look
            Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 moveDirectionRelativeToCamera = cameraTransform.TransformDirection(moveDirection);
            Vector3 directionToMove = Vector3.ProjectOnPlane(moveDirectionRelativeToCamera, Vector3.up);

            // Rotate the player to face the direction of movement
            if (directionToMove.magnitude >= 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToMove);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        // Move the player based on input
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDirectionRelativeToCamera = cameraTransform.TransformDirection(moveDirection);
        Vector3 directionToMove = Vector3.ProjectOnPlane(moveDirectionRelativeToCamera, Vector3.up);

        Vector3 movement = directionToMove * moveSpeed;
        transform.position += movement * Time.fixedDeltaTime;
    }
}

