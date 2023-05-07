using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Fix2 : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float speed;
    [SerializeField]  float horizontalInput;
    [SerializeField] float rotationSpeed;
    [SerializeField] CinemachineFreeLook freeLookCam;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //inputs
        horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //make direction in Vector3
        Vector3 cameraForward = freeLookCam.transform.forward;
        cameraForward.y = 0; // make sure the camera's forward direction is horizontal
        Vector3 moveDirection = (horizontalInput * freeLookCam.transform.right + verticalInput * cameraForward).normalized;

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
        //movement Base on Animator Tree Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = freeLookCam.transform.forward;
        cameraForward.y = 0;
        Vector3 movement = (horizontalInput * freeLookCam.transform.right + verticalInput * cameraForward).normalized * speed;
        transform.position += movement * Time.fixedDeltaTime;
    }

    void LateUpdate()
    {
        float yRotation = transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, yRotation, 0);
        freeLookCam.transform.rotation = rotation * Quaternion.Euler(0, horizontalInput, 0);
    }


}