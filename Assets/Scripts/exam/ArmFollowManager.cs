using UnityEngine;

public class ArmFollowManager : MonoBehaviour
{
    [Tooltip("The arm that follows first (right hand)")]
    public ArmFollower rightArm;

    [Tooltip("The other arm to swap in (left hand)")]
    public ArmFollower leftArm;

    void Start()
    {
        // On start: right follows, left does not
        if (rightArm != null) rightArm.StartFollowing();
        if (leftArm  != null) leftArm.StopFollowing();
    }

    void Update()
    {
        if (rightArm == null || leftArm == null) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // if right is active, switch to left; otherwise switch back
            if (rightArm.isFollowing)
            {
                rightArm.StopFollowing();
                leftArm.StartFollowing();
                Debug.Log("Switched to LEFT arm");
            }
            else
            {
                leftArm.StopFollowing();
                rightArm.StartFollowing();
                Debug.Log("Switched to RIGHT arm");
            }
        }
    }
}