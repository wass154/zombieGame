using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIkTest : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] Vector3 Offset;
    [SerializeField] Transform CamPos;
    [SerializeField] Transform Aim;
    [SerializeField] Transform Bone;
    [SerializeField] Transform Camera;
    [SerializeField] int iterations;
    [Range(0, 1)]
    [SerializeField] float Weight;

    private bool isAimingUpOrDown;


    [SerializeField] float Val;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

      



        if (PickUp.isUse)
        {
            Target = CamPos;
        }
        else
        {
            Target = null;
        }

    }
    private void LateUpdate()
    {
        Vector3 TargetPos = Target.position;
        for (int i = 0; i < iterations; i++)
        {
            AimToTarget(Bone, TargetPos);
        }
    }
    private void AimToTarget(Transform Bone, Vector3 TargetPos)
    {
        Vector3 AimDirection = Aim.forward;
  
        Vector3 TargetDirection = TargetPos - Aim.position;
        TargetDirection.Normalize();

        Quaternion AimRot = Quaternion.FromToRotation(AimDirection, TargetDirection);
     Bone.rotation = AimRot * Bone.rotation;
    


    }
}


