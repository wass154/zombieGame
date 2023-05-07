using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class Ik : MonoBehaviour
{
    public Transform Target;
    public Transform Aim;
    public Transform Bone;

    public float AngleLimit;
    public float DistanceLimit;
    public int iteration=10;
    [Range(0,1)]
    public float weight;
    public Vector3 Offset;

    public float maxRotationAngle;





    // Start is called before the first frame update
    void Start()
    {
     
    }

    Vector3 GetTargetPos()
    {
Vector3 targetDirection= Target.position-Aim.position;
        Vector3 aimDirection = Aim.forward;
        float Blend = 0f;
        float TargetAngle=Vector3.Angle(targetDirection, aimDirection);
        if(TargetAngle > AngleLimit)
        {
            Blend += (TargetAngle - AngleLimit) / 50f;
        }
        float X = Input.GetAxis("Mouse X");
        float Y = Input.GetAxis("Mouse Y");
        /*
        if (Y != 0 && Y>0)
        {
            Offset.y=Mathf.Lerp(Offset.y,40f,Time.deltaTime);
            if (Y == 0)
            {
                float F = Offset.y;
            }
           
        }
        else
        {
            Offset.y = Mathf.Lerp(Offset.y, -40f, Time.deltaTime);
            if (Y == 0)
            {
                float f = Offset.y;
            }
        }
        */

        float targetDistance =targetDirection.magnitude;
        if(targetDistance< DistanceLimit)
        {
            Blend += DistanceLimit - targetDistance;
        }
        Vector3 direction=Vector3.Slerp(targetDirection,aimDirection,Blend);
        
        return Aim.position + direction + Offset;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
  private void LateUpdate()
    {
        Vector3 TargetPos = GetTargetPos();
        for (int i = 0; i < iteration;i++)
        {
           
                AimTarget(Bone, TargetPos, weight);
             //  AimTargetTwo(Bone, TargetPos, weight, maxRotationAngle);
                    
        }
        
    }
    private void AimTarget(Transform Bone,Vector3 TargetPos,float weight)
    {
        Vector3 aimDirection = Aim.forward;
      //  Vector3 TargetDirection = TargetPos - aimDirection;
        Vector3 TargetDirection = TargetPos - Aim.position;
        Quaternion AimRot=Quaternion.FromToRotation(aimDirection, TargetDirection);
        Quaternion BlendRot = Quaternion.Slerp(Quaternion.identity, AimRot, weight);
        Bone.rotation = BlendRot*Bone.rotation;
    }


    private void AimTarget(Transform Bone, Vector3 TargetPos, float weight, float maxRotationAngle, float targetRotationAngle)
    {
        Vector3 aimDirection = Aim.forward;
        Vector3 TargetDirection = TargetPos - Aim.position;
        Quaternion AimRot = Quaternion.FromToRotation(aimDirection, TargetDirection);
        Quaternion BlendRot = Quaternion.Slerp(Quaternion.identity, AimRot, weight);

        // Check if the bone's rotation angle has exceeded the maximum allowed angle
        float currentRotationAngle = Quaternion.Angle(Bone.rotation, BlendRot * Bone.rotation);
        if (currentRotationAngle > maxRotationAngle)
        {
            // Smoothly interpolate towards the desired angle
            Quaternion targetRotation = Quaternion.RotateTowards(Bone.rotation, BlendRot * Bone.rotation, targetRotationAngle);
            Bone.rotation = Quaternion.Lerp(Bone.rotation, targetRotation, Time.deltaTime);
        }
        else
        {
            // Smoothly rotate the bone towards the target rotation
            Bone.rotation = Quaternion.RotateTowards(Bone.rotation, BlendRot * Bone.rotation, maxRotationAngle);
        }
    }
}
