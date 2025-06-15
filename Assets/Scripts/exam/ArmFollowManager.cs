using UnityEngine;

public class ArmFollowManager : MonoBehaviour
{
    public Animator animator;
    public ArmFollower rightArm;
    public ArmFollower leftArm;

    void Start()
    {
        rightArm.StartFollowing();
        leftArm.StopFollowing();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rightArm.isFollowing)
            {
                animator.SetBool("blockleft", true);
                rightArm.StopFollowing();
                leftArm.StartFollowing();
                Debug.Log("Switched to LEFT arm");
            }
            else
            {
                animator.SetBool("blockleft", false);
                leftArm.StopFollowing();
                rightArm.StartFollowing();
                Debug.Log("Switched to RIGHT arm");
            }
        }
    }
}