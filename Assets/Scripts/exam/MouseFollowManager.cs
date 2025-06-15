using UnityEngine;

public class MouseFollowManager : MonoBehaviour
{
    public MouseLimbFollower limbToControl;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (limbToControl != null)
                limbToControl.StartFollowing();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (limbToControl != null)
                limbToControl.StopFollowing();
        }
    }
}