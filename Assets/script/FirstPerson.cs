using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FirstPerson : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float mouseSensitivity = 3f;
    private float verticalLookRotation;
    [SerializeField] float min, max;
    [SerializeField] float smoothSpeed = 10f;

    public static float X, Y;


    // Start is called before the first frame update
    void Start()
    {
       // EnableDisable.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!transform.gameObject.activeInHierarchy)
        {
            // Activate the game object if it's not active in the hierarchy
            transform.gameObject.SetActive(true);
        }
      




    }
    private void LateUpdate()
    {
       SetupCamera();
    }
    void SetupCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        X = mouseX;
        Y=mouseY;

        verticalLookRotation += mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, min, max);

        Vector3 targetRotation = new Vector3(-verticalLookRotation, 0f, 0f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(targetRotation), Time.deltaTime * smoothSpeed);

       Player.transform.eulerAngles += new Vector3(0f, mouseX, 0f);
    }
}
