using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copywalker : MonoBehaviour
{
    public Transform targetLimb;
    public bool mirror=false;
    private ConfigurableJoint cj;
    private Quaternion initialLocalRotation;

    void Start()
    {
        cj = GetComponent<ConfigurableJoint>();

        // Store initial local rotation (T-pose) of the ragdoll limb
        initialLocalRotation = transform.localRotation;
    }

    void FixedUpdate()  // Use FixedUpdate for physics consistency
    {
        if (targetLimb == null || cj == null) return;

        // Get the local rotation of the animated limb (relative to its parent)
        Quaternion targetLocalRot = targetLimb.localRotation;

        // Apply mirroring if needed
        if (mirror)
        {
            targetLocalRot = Quaternion.Inverse(targetLocalRot);
        }

        // Compute the joint-space target rotation
        Quaternion jointTargetRotation = Quaternion.Inverse(targetLocalRot) * initialLocalRotation;

        cj.targetRotation = jointTargetRotation;
    }
}