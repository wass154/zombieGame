using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSETUP : MonoBehaviour
{
    public Transform Target;
    public Transform Aim;
    public Transform Bone;

    public float AngleLimit;
    public float DistanceLimit;
    public int iteration = 10;
    [Range(0, 1)]
    public float weight;
    public Vector3 Offset;
    public float offsetYMax = 40f; // maximum Y offset when looking up
    public float offsetYMin = -40f; // minimum Y offset when looking down
    public float offsetYLerpSpeed = 5f; // speed at which to lerp the Y offset
    private float currentOffsetY = 0f; // current Y offset
    private Vector3 originalOffset; // original offset value

    // Start is called before the first frame update
    void Start()
    {
        originalOffset = Offset;
    }

    Vector3 GetTargetPos()
    {
        Vector3 targetDirection = Target.position - Aim.position;
        Vector3 aimDirection = Aim.forward;
        float Blend = 0f;
        float TargetAngle = Vector3.Angle(targetDirection, aimDirection);
        if (TargetAngle > AngleLimit)
        {
            Blend += (TargetAngle - AngleLimit) / 50f;
        }

        float targetDistance = targetDirection.magnitude;
        if (targetDistance < DistanceLimit)
        {
            Blend += DistanceLimit - targetDistance;
        }

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, Blend);
        Vector3 finalOffset = originalOffset + new Vector3(0f, currentOffsetY, 0f);

        return Aim.position + direction + finalOffset;
    }

    // Update is called once per frame
    void Update()
    {
        // If player is not aiming, reset current offset value
        if (Mathf.Abs(Input.GetAxis("Mouse X")) < 0.1f && Mathf.Abs(Input.GetAxis("Mouse Y")) < 0.1f)
        {
            currentOffsetY = Mathf.Lerp(currentOffsetY, 0f, Time.deltaTime * offsetYLerpSpeed);
        }
        // Otherwise, update current offset value based on player's aiming direction
        else
        {
            float mouseY = Input.GetAxis("Mouse Y");
            float targetOffsetY = Mathf.Clamp(currentOffsetY + mouseY, offsetYMin, offsetYMax);
            currentOffsetY = Mathf.Lerp(currentOffsetY, targetOffsetY, Time.deltaTime * offsetYLerpSpeed);
        }
    }

    private void LateUpdate()
    {
        Vector3 TargetPos = GetTargetPos();
        for (int i = 0; i < iteration; i++)
        {
            AimTarget(Bone, TargetPos, weight);
        }
    }

    private void AimTarget(Transform Bone, Vector3 TargetPos, float weight)
    {
        Vector3 aimDirection = Aim.forward;
        Vector3 TargetDirection = TargetPos - Aim.position;
        Quaternion AimRot = Quaternion.FromToRotation(aimDirection, TargetDirection);
        Quaternion BlendRot = Quaternion.Slerp(Quaternion.identity, AimRot, weight);
        Bone.rotation = BlendRot * Bone.rotation;
    }
}