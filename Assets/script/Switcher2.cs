using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher2 : MonoBehaviour
{
    [SerializeField] GameObject CameraObject;
    [SerializeField] bool isCameraActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //CameraObject.SetActive(false);
            print("disable");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Toggle camera enabled flag
                isCameraActive = !isCameraActive;

                // Activate/deactivate the camera based on the flag
                CameraObject.gameObject.SetActive(isCameraActive);
            }
        }
    }
}
