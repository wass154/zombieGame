using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] GameObject CameraObject;
    [SerializeField] GameObject CameraObject2;
    [SerializeField] bool isCameraActive = true;
    public static bool isT;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //CameraObject.SetActive(false);
            print("disable");
           
           
                // Toggle camera enabled flag
                isCameraActive = !isCameraActive;
            isT= isCameraActive;
                // Activate/deactivate the camera based on the flag
                CameraObject.gameObject.SetActive(isCameraActive);
            
        }
        /*
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isCameraActive = !isCameraActive;
            CameraObject2.gameObject.SetActive(isCameraActive);
        }
        */
    }
}
