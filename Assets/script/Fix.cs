using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fix: MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] Camera Cam;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //make direction in Vector3
        Vector3 cameraForward = Cam.transform.forward;
        cameraForward.y = 0; // make sure the camera's forward direction is horizontal
        Vector3 moveDirection = (horizontalInput * Cam.transform.right + verticalInput * cameraForward).normalized;

        //rotation player
        if (moveDirection.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //inputs to Animator BlendTree
        anim.SetFloat("inputX", horizontalInput);
        anim.SetFloat("inputY", verticalInput);
    }

    private void FixedUpdate()
    {
        CameraSetup();

        //movement Base on Animator Tree Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = Cam.transform.forward;
        cameraForward.y = 0;
        Vector3 movement = (horizontalInput * Cam.transform.right + verticalInput * cameraForward).normalized * speed;
        transform.position += movement * Time.fixedDeltaTime;
    }

    void CameraSetup()
    {
        float YCam = Cam.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, YCam, 0), rotationSpeed * Time.fixedDeltaTime);
        
    }
}