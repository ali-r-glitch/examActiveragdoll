using UnityEngine;

public class SmoothFollower : MonoBehaviour
{
    [Header("Target to follow")]
    public Transform target;

    [Header("Follow settings")]
    public Vector3 positionOffset = new Vector3(0, 5, -10);
    public float followSmoothTime = 0.2f;

    [Header("Axis locking")]
    public bool followX = true;
    public bool followY = true;
    public bool followZ = true;

    private Vector3 currentVelocity;

    private void LateUpdate()
    {
        if (target == null) return;

        // Desired position based on offset
        Vector3 desiredPosition = target.position + positionOffset;

        // Lock axes you don't want to follow
        Vector3 currentPosition = transform.position;

        if (!followX) desiredPosition.x = currentPosition.x;
        if (!followY) desiredPosition.y = currentPosition.y;
        if (!followZ) desiredPosition.z = currentPosition.z;

        // Smoothly move there
        transform.position = Vector3.SmoothDamp(currentPosition, desiredPosition, ref currentVelocity, followSmoothTime);
    }
}