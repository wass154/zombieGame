using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
[SerializeField] GameObject gun;

    private bool hasGun = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !hasGun)
        {
            gun.transform.SetParent(null);
            gun.transform.position = transform.position + transform.forward;
            hasGun = false;
            GetComponent<Animator>().SetBool("HasGun", false);
        }
    }
}
