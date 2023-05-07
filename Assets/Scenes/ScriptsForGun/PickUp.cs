using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
   [SerializeField] GameObject gun;
    [SerializeField] Transform hand;
    [SerializeField] Vector3 localpos;
    [SerializeField] Quaternion Rot;
    public static bool isUse;
    private bool hasGun = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !hasGun)
        {
            gun.transform.SetParent(hand);
            gun.transform.localPosition = Vector3.zero;
            gun.transform.localRotation = Quaternion.identity;
            hasGun = true;
            GetComponent<Animator>().SetBool("HasGun", true);
            isUse=true;
        }
        if (Input.GetKeyDown(KeyCode.E) && hasGun)
        {
           // gun.transform.SetParent(null);
           // gun.transform.position = transform.position + transform.forward;
            hasGun = false;
            GetComponent<Animator>().SetBool("HasGun", false);
            isUse = false;
        }
    }

}

