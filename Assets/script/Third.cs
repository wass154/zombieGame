using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;



public class Third : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] Vector2 input;
    [SerializeField]float speed;
    [SerializeField] float RunSpeed = 10f;
    [SerializeField] float WalkSpeed = 7f;
    [SerializeField]float rotationSpeed;
    [SerializeField]  float jumpForce = 10f;
    [SerializeField] float groundDistance = 0.2f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Rigidbody rb;
    private bool isGrounded;

    [SerializeField] Camera Cam;
    public CinemachineFreeLook freeLookCamera;
    public bool rotateOnXAxis = false;
    public bool rotateOnYAxis = true;
    public bool rotateOnZAxis = false;
    [SerializeField] Vector3 movement;

    public bool canRotate = true;
    [SerializeField] float currentRot;

    float vel;
    [SerializeField]
    float smooth;


    //Stamina
    [SerializeField] Image fillImage;
    [SerializeField] float maxStamina = 100f;
    [SerializeField] float currentStamina;
    [SerializeField] float staminaDrainRate = 20f;
    [SerializeField] float staminaRegenRate = 10f;
    private bool isRunning;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentStamina = maxStamina;
        anim = GetComponent<Animator>();
        Cam=Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Stamina()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0;

        if (isRunning)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
        }
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        fillImage.fillAmount = currentStamina / maxStamina;

        if (isRunning)
        {
            speed = RunSpeed;
            anim.SetFloat("Speed", 1f);

        }
        else
        {
            speed = WalkSpeed;
            anim.SetFloat("Speed", 0.5f, 0.5f, 0.2f);

        }
        if (currentStamina <= 0f)
        {
            speed = WalkSpeed;
            anim.SetFloat("Speed", 0.5f, 0.5f, 0.2f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(movement != Vector3.zero)
        {
            Stamina();
        }
        else
        {
            anim.SetFloat("Speed", 0f, 0.5f, 0.2f);
        }


        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
          
            anim.SetBool("Jump", false);
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {

            anim.SetBool("Jump", true);
           
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if (!isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        //inputs
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        //make direction in Vector3

    // Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized;
        /*
        //rotation player
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        */
        //inputs to Animator BlendTree
        anim.SetFloat("inputX", input.x);
        anim.SetFloat("inputY", input.y);
        // FreeLock();
        // Look() ;

        if (movement == Vector3.zero)
        {

            rotationSpeed = 0;
        }
        else if (movement != Vector3.zero)
        {
            rotationSpeed = currentRot;
        }
    // AimTree();
       

    }
    private void FixedUpdate()
    {
        

        if (Switcher.isT)
        {
          
        }
        else
        {
            CameraSetup();
        }
     
        //movement Base on Animator Tree Movement
        movement = transform.forward * input.y + transform.right * input.x;

        // Move the player in the movement direction
       transform.position += movement.normalized * speed * Time.fixedDeltaTime;

       
    }
 void CameraSetup()
    {
         float YCam = Cam.transform.rotation.eulerAngles.y;
       transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0  , YCam, 0), rotationSpeed * Time.deltaTime);
        
    }
    void FreeLock()
    {
        // Get the camera's forward direction and remove the y-component
        Vector3 cameraForward = freeLookCamera.transform.forward;
        cameraForward.y = 0;

        // Rotate the players to face the camera's forward direction
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        Vector3 euler = targetRotation.eulerAngles;

        if (!rotateOnXAxis) euler.x = transform.rotation.eulerAngles.x;
        if (!rotateOnYAxis) euler.y = transform.rotation.eulerAngles.y;
        if (!rotateOnZAxis) euler.z = transform.rotation.eulerAngles.z;

        transform.rotation = Quaternion.Euler(euler);
    }
    void Look()
    {
        if (canRotate)
        {
            // Get the camera's forward direction and remove the y-component
            Vector3 cameraForward = freeLookCamera.transform.forward;
            cameraForward.y = 0;

            // Rotate the player to face the camera's forward direction
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = targetRotation;
        }
        else
        {
            // Rotate the player based on the mouse's X and Y axis
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Limit the rotation on the Y axis to prevent the player from looking too far up or down
            float currentYRotation = transform.rotation.eulerAngles.y;
            float newYRotation = currentYRotation + mouseX;
            float currentXRotation = transform.rotation.eulerAngles.x;
            float newXRotation = currentXRotation - mouseY;
            newXRotation = Mathf.Clamp(newXRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(newXRotation, newYRotation, 0f);
        }
    }
   
    void AimTree()
    {
        if (movement != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            speed = WalkSpeed;
            anim.SetFloat("Speed", 0.5f, 0.5f, 0.2f);
        }
        else if (movement != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
           speed = RunSpeed;
            anim.SetFloat("Speed", 1f);

          
        }
        else
        {
            anim.SetFloat("Speed", 0f, 0.5f, 0.2f);
        }

       
    }



   

}


