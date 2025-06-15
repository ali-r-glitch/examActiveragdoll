using UnityEngine;

public class MouseLimbFollower : MonoBehaviour
{
    [SerializeField] private float followForce = 50f;
    [SerializeField] private float maxDistance = 2f;

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
        float depth = Mathf.Abs(mainCam.transform.position.z - transform.position.z);
        Vector3 targetPos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));

        Vector3 direction = targetPos - rb.position;

        rb.AddForce(direction * followForce, ForceMode.Force);

        if (direction.magnitude > maxDistance)
        {
            direction = direction.normalized * maxDistance;
            rb.position += direction;
        }
    }

    public void StartFollowing() => isFollowing = true;
    public void StopFollowing() => isFollowing = false;
    public void SetFollowForce(float force) => followForce = force;
    public void SetMaxDistance(float dist) => maxDistance = dist;
}
