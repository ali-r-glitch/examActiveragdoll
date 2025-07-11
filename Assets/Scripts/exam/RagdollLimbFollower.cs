using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RagdollLimbFollower : MonoBehaviour
{
    [SerializeField] private float followForce = 50f;
    [SerializeField] private float maxDistance = 2f;
    [SerializeField] private float heightOffset = 1f; // Fixed Y height to maintain

    private Camera mainCam;
    private Rigidbody rb;
    private bool isFollowing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }

    void FixedUpdate()
    {
        if (!isFollowing || mainCam == null) return;

        Vector3 mousePos = Input.mousePosition;

        // Project mouse into world space
        float depth = Mathf.Abs(mainCam.transform.position.z - transform.position.z);
        Vector3 targetWorldPos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));

        // Use fixed height (Y) — not based on current ragdoll position
        float targetY = heightOffset;
        Vector3 targetPos = new Vector3(targetWorldPos.x, targetY, targetWorldPos.z);

        // Get direction toward target
        Vector3 direction = targetPos - rb.position;

        // Clamp distance
        if (direction.magnitude > maxDistance)
            direction = direction.normalized * maxDistance;

        // Apply smooth force to move limb toward mouse
        Vector3 smoothedForce = direction * followForce * Time.fixedDeltaTime;
        rb.AddForce(smoothedForce, ForceMode.VelocityChange);
    }

    // Public API to toggle following
    public void StartFollowing() => isFollowing = true;
    public void StopFollowing() => isFollowing = false;

    // Optional control setters
    public void SetFollowForce(float force) => followForce = force;
    public void SetMaxDistance(float dist) => maxDistance = dist;
    public void SetHeightOffset(float y) => heightOffset = y;
}