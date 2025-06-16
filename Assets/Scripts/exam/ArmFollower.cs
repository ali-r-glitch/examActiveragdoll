using UnityEngine;

public class ArmFollower : MonoBehaviour
{
    public float lerpSpeed = 5f;
    public float maxDistance = 2f;

    private Rigidbody rb;
    private Vector2 inputDirection;
    private Camera mainCam;
    public bool isFollowing = false;

    public void SetDirection(Vector2 dir)
    {
        inputDirection = dir;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }

    void FixedUpdate()
    {
        if (!isFollowing || inputDirection == Vector2.zero) return;

        // Use camera-facing XZ world direction
        Vector3 camForward = mainCam.transform.forward;
        Vector3 camRight = mainCam.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 worldDir = camForward * inputDirection.y + camRight * inputDirection.x;
        Vector3 targetPos = rb.position + worldDir.normalized * maxDistance;

        Vector3 newPos = Vector3.Lerp(rb.position, targetPos, lerpSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    public void StartFollowing() => isFollowing = true;
    public void StopFollowing() => isFollowing = false;
}