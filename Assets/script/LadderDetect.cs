using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LadderDetect : MonoBehaviour
{

    public Transform topPosition;
    public float climbSpeed = 5f;
    public float climbDistance = 1f;

    public Animator animator;
    private bool isClimbing = false;
    private float currentClimbDistance = 0f;
    public Rigidbody rb;



    private float startY;

    // Set the amount of movement for each key press
    public float stepSize = 5;

    // Set the duration of the lerp effect
    public float lerpDuration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startY = transform.position.y;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        if (isClimbing)
        {


        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            float endY = transform.position.y + stepSize;
            StartCoroutine(LerpY(endY));
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            float endY = transform.position.y - stepSize;
            StartCoroutine(LerpY(endY));
        }
    }
        // Update is called once per frame
        void Update()
        {
          
            }

            void OnTriggerEnter(Collider other)
            {
                // Check if the player has collided with the ladder
                if (other.CompareTag("Ladder"))
                {
                    animator.SetBool("isClimbing", true);
                    // Cast a ray from the player's position to the ladder
                    RaycastHit hit;
                    Vector3 direction = transform.position - other.transform.position;
                    if (Physics.Raycast(other.transform.position, direction, out hit))
                    {
                        // If the ray hits the ladder, start the climbing animation
                        if (hit.collider.gameObject == gameObject)
                        {
                            isClimbing = true;

                            animator.SetBool("isClimbing", true);

                            rb.useGravity = false;
                        }
                    }
                }
            }
            void OnTriggerExit(Collider other)
            {
                // If the player exits the ladder, stop the climbing animation
                if (other.CompareTag("Ladder"))
                {
                    isClimbing = false;

                    animator.SetBool("isClimbing", false);

                    rb.useGravity = true;
                }
            }
    private IEnumerator LerpY(float endY)
    {
        float startTime = Time.time;
        float elapsedTime = 0;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(startY, endY, t), transform.position.z);

            elapsedTime = Time.time - startTime;
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, endY, transform.position.z);
        startY = endY;
    }

}




