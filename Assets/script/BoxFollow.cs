using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector3 offset;
    private Animator animator;

    [SerializeField] AnimationClip attackAnimationClip;
    // Start is called before the first frame update
    void Start()
    {
        animator = player.GetComponent<Animator>();
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimationClip.name))
        {
            transform.position = player.transform.position + offset;
        }
    }
}
