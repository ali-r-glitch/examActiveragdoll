using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forces : MonoBehaviour
{
    
   float upforces=20f;
   float downforces=10f;
    private bool isrightarm;

    
    public Rigidbody rightUpperArm;
    public Rigidbody rightForearm;
    public Rigidbody leftUpperArm;
    public Rigidbody leftForearm;
    public Rigidbody rbhead;
    public Rigidbody rbfoot;
    public Rigidbody lbfootl;
    private RagdollLimbFollower _limbFollower;  
    public float torqueStrength = 100f;
    [SerializeField] private float positionforce;
    private bool isRightArm = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRightArm = !isRightArm;
            
        }
        _limbFollower.FollowMouse(Camera.main);

    }

    private void Start()
    {
        _limbFollower = new RagdollLimbFollower(leftUpperArm, followForce: 60f, maxDistance: 2f);

    }
    
    private void FixedUpdate()
    {
        // if (isRightArm)
        // {
        //     ApplyBoxingPose(rightUpperArm, Quaternion.Euler(-90, 0, 45));
        //     ApplyBoxingPose(rightForearm, Quaternion.Euler(-90, 0, 10));
        // }
        // else
        // {
        //     ApplyBoxingPose(leftUpperArm, Quaternion.Euler(-90, 0, -45));
        //     ApplyBoxingPose(leftForearm, Quaternion.Euler(-90, 0, -10));
        // }
        
        if (isRightArm)
            { 
                //rightUpperArm.AddForce(-Vector3.up * upforces, ForceMode.Impulse);
                rightForearm.AddForce(positionforce*Vector3.up * upforces, ForceMode.Impulse);
            }
            else
            {
                //leftUpperArm.AddForce(-Vector3.up * upforces, ForceMode.Impulse);
                leftForearm.AddForce(positionforce*Vector3.up * upforces, ForceMode.Impulse);
            }
        rbhead.AddForce(Vector3.up * (upforces), ForceMode.Impulse);
        rbfoot.AddForce(-Vector3.up * (downforces+positionforce), ForceMode.Impulse);
        lbfootl.AddForce(-Vector3.up *( downforces+positionforce), ForceMode.Impulse);
    }

    void ApplyBoxingPose(Rigidbody limb, Quaternion targetLocalRotation)
    {
        Quaternion currentLocalRotation = limb.transform.localRotation;
        Quaternion deltaRotation = targetLocalRotation * Quaternion.Inverse(currentLocalRotation);

        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

        // Safety check
        if (float.IsNaN(axis.x)) return;

        Vector3 torque = axis * angle * torqueStrength * Mathf.Deg2Rad; // Convert degrees to radians
        limb.AddTorque(torque, ForceMode.Acceleration);
    }
}

   

