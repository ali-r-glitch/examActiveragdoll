using UnityEngine;
using UnityEngine.InputSystem;

public class ArmFollowManager : MonoBehaviour
{
    public ArmFollower rightArm;
    public ArmFollower leftArm;

    private Vector2 aimInput;

    public void OnControlarm(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        Debug.Log("Right Stick (controlarm): " + dir);
        aimInput = dir;
    }


    public void OnSwitchArm(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (rightArm.isFollowing)
        {
            rightArm.StopFollowing();
            leftArm.StartFollowing();
        }
        else
        {
            leftArm.StopFollowing();
            rightArm.StartFollowing();
        }
    }

    void Update()
    {
        if (rightArm.isFollowing)
            rightArm.SetDirection(aimInput);
        else if (leftArm.isFollowing)
            leftArm.SetDirection(aimInput);
    }
}