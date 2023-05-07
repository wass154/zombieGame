using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{

    public float climbSpeed = 5f;
    public Transform topPosition;

    private Animator animator;
    private bool isClimbing = false;
    private Collider climbingTrigger;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ClimbingSurface")
        {
            climbingTrigger = other;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other == climbingTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("StartClimbing");
            isClimbing = true;
        }
    }

    void FixedUpdate()
    {
        if (isClimbing)
        {
            transform.Translate(Vector3.up * climbSpeed * Time.deltaTime);
            if (transform.position.y >= topPosition.position.y)
            {
                transform.position = topPosition.position;
                animator.SetTrigger("StopClimbing");
                isClimbing = false;
                climbingTrigger = null;
            }
        }
    }

}
