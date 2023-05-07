using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Transform topPosition;
    public float climbSpeed = 5f;
    public float climbDistance = 1f;

    private Animator animator;
    private bool isClimbing = false;
    private float currentClimbDistance = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // If the player is climbing, move them up the ladder
        if (isClimbing)
        {
            currentClimbDistance += climbSpeed * Time.deltaTime;
            float percent = currentClimbDistance / climbDistance;
            transform.position = Vector3.Lerp(transform.position, topPosition.position, percent);

            // If the player has reached the top, stop climbing
            if (percent >= 1f)
            {
                isClimbing = false;
                animator.SetBool("isClimbing", false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player has collided with the ladder
        if (other.CompareTag("Ladder"))
        {
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
                }
            }
        }
    }
}