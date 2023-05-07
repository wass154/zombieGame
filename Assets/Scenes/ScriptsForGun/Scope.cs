using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    [SerializeField] float Smothness;
    [SerializeField] Vector3 scopePos;
    [SerializeField] Vector3 CureentPos;
    public bool isScoping;

    [SerializeField] Camera fpsCamera;
    private float defaultFov;
    [SerializeField] float aimFov = 40f;
    [SerializeField] float aimSpeed = 40f;

    [SerializeField] Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        CureentPos=transform.localPosition;
        defaultFov = fpsCamera.fieldOfView;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1) )
        {
            isScoping = true;
            //  transform.localRotation= Quaternion.identity;
            fpsCamera.fieldOfView = Mathf.Lerp(fpsCamera.fieldOfView, aimFov, aimSpeed * Time.deltaTime);

            transform.localPosition = Vector3.Lerp(transform.localPosition, scopePos, Smothness * Time.deltaTime);
            anim.SetTrigger("SCOPE");
        }
        else
        {
            isScoping = false;
            // transform.localRotation = Quaternion.identity;
            fpsCamera.fieldOfView = Mathf.Lerp(fpsCamera.fieldOfView, defaultFov, aimSpeed * Time.deltaTime);

            transform.localPosition = Vector3.Lerp(transform.localPosition, CureentPos, Smothness * Time.deltaTime);
            //   transform.localPosition = CureentPos;
          
        }
    










    }
}
