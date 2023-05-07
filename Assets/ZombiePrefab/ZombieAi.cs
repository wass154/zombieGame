using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAi : MonoBehaviour
{
    [SerializeField] float attackDistance = 1f;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] Transform player;
    [SerializeField]  Animator animator;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float maxAngle = 60f;
    [SerializeField] float waypointStoppingDistance = 0.1f;
    private bool useWaypoints = false;
    [SerializeField] Transform[] waypoints;
    [SerializeField] float waitTime = 2f;
    [SerializeField] Vector3 Offset;

    private bool isZombie = false;
    private Quaternion targetRotation;
    private int currentWaypoint = 0;
    private float distanceToPlayer;
    private float angleToPlayer;
    private bool playerInSight;
    private int currentWaypointIndex;
    private Vector3 currentWay;
 private float timeSinceLastAttack = Mathf.Infinity;

    public static bool isAttack;

    [SerializeField] float damageAmount = 10f;
    [SerializeField] Blood blood;

    [SerializeField] AnimationClip attackAnimationClip;
    private bool loopTime = true;
    // Start is called before the first frame update
    void Start()
    {
        //setup waypoints
        if (waypoints != null && waypoints.Length > 0)
        {
            currentWaypointIndex = 0;
            currentWay = waypoints[currentWaypointIndex].position;
        }
        animator=GetComponent<Animator>();  
    }
    IEnumerator LoopAnimation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            loopTime = !loopTime;
            animator.SetBool("A", loopTime);
            isAttack=true;
            yield return new WaitForSeconds(1);
        }
       
    }
    // Update is called once per frame
    void Update()
    {

     
                 





        if (animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimationClip.name))
        {
            // Check if the attack animation has finished
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                blood.TakeDamage(damageAmount);
            }
        }




                // if player exist
                if (player != null)
        {
            //calculate distance and angle with player
            distanceToPlayer = Vector3.Distance(player.position, transform.position);
            angleToPlayer = Vector3.Angle(transform.forward, player.position - transform.position);

            //make a raycast on player if hit then see else not
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Offset, player.position - transform.position, out hit, maxDistance))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    playerInSight = true;
                }
                else
                {
                    playerInSight = false;
                }
                Debug.DrawLine(transform.position + Offset, hit.point, Color.red);
            }
            else
            {
                playerInSight = false;
            }
            //check if raycast and player on zombie vue and distance quite small then attack if not then follow player or if not then patrol waypoints 
            if (playerInSight && angleToPlayer < maxAngle)
            {
                if (distanceToPlayer <= attackDistance)
                {
                    /*
                    // Attack the player
                    if (timeSinceLastAttack >= timeBetweenAttacks)
                    {
                        animator.SetTrigger("Attack");

                        timeSinceLastAttack = 0f;

                        //   blood.TakeDamage(damageAmount);
                    }
                    else
                    {

                        animator.SetBool("Running", true);

                        //  animator.ResetTrigger("Attack");
                    }

                    */
                    //  animator.SetTrigger("Attack");
                    //   animator.SetBool("A", true);

                    StartCoroutine(LoopAnimation());
                }
                else
                {
                    // Follow the player
                    targetRotation = Quaternion.LookRotation(player.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                    transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                    animator.SetBool("Running", true);
                    animator.SetBool("A", false);

                }
            }
            else
            {
                animator.SetBool("Attack", false);
                animator.SetBool("Running", true);
                // Patrol the waypoints
                if (waypoints != null && waypoints.Length > 0)
                {
                    float distanceToWaypoint = Vector3.Distance(currentWay, transform.position);
                    if (distanceToWaypoint <= waypointStoppingDistance)
                    {
                        currentWaypointIndex++;
                        if (currentWaypointIndex >= waypoints.Length)
                        {
                            currentWaypointIndex = 0;
                        }
                        currentWay = waypoints[currentWaypointIndex].position;
                    }
                    else
                    {
                        targetRotation = Quaternion.LookRotation(currentWay - transform.position);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                    }
                }
            }

            timeSinceLastAttack += Time.deltaTime;
        }
    }

}

