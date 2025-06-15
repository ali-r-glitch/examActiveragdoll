using UnityEngine;


public class ArmFollower : MonoBehaviour
{
    public float lerpSpeed = 5f;          // How fast the arm moves
    public float maxDistance = 2f;        // How far it can reach

    private Rigidbody rb;
    private Camera mainCam;
    public bool isFollowing = false;      // Controlled externally

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }

    void FixedUpdate()
    {
        if (!isFollowing || mainCam == null) return;

        Vector3 mousePos = Input.mousePosition;

        // Estimate depth from camera to arm using actual distance
        float depth = Vector3.Distance(mainCam.transform.position, transform.position);
        Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));

        // Limit distance
        Vector3 direction = worldPos - rb.position;
        if (direction.magnitude > maxDistance)
            worldPos = rb.position + direction.normalized * maxDistance;

        // Move smoothly
        Vector3 newPos = Vector3.Lerp(rb.position, worldPos, lerpSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    public void StartFollowing() => isFollowing = true;
    public void StopFollowing()  => isFollowing = false;
}