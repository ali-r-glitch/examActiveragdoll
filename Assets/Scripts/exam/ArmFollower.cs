using UnityEngine;


public class ArmFollower : MonoBehaviour
{
    public float lerpSpeed = 5f;          // How fast the arm moves
    public float maxDistance = 2f;        // How far it can reach

    private Rigidbody rb;
    public Camera mainCam;
    public bool isFollowing = false;      // Controlled externally

    public void movearm(Vector3 mousePos)
    {
       

        // // Estimate depth from camera to arm using actual distance
        // float depth = Vector3.Distance(mainCam.transform.position, transform.position);
        // Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
        //
        // // Limit distance
        // Vector3 direction = worldPos - rb.position;
        // if (direction.magnitude > maxDistance)
        //     worldPos = rb.position + direction.normalized * maxDistance;
        //
        // // Move smoothly
       // Vector3 newPos = Vector3.Lerp(rb.position, worldPos, lerpSpeed * Time.fixedDeltaTime);
       Vector3 newPos = Vector3.Lerp(rb.position, (new Vector3(transform.position.x+mousePos.x,transform.position.y,transform.position.z+mousePos.z)),lerpSpeed * Time.fixedDeltaTime);
       
        rb.MovePosition(newPos);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
      //  mainCam = Camera.main;
    }

    void FixedUpdate()
    {
       // if (!isFollowing || mainCam == null) return;


    }

    public void StartFollowing() => isFollowing = true;
    public void StopFollowing()  => isFollowing = false;
}