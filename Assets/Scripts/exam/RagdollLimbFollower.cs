using UnityEngine;

public class RagdollLimbFollower
{
    private Rigidbody _limb;
    private float _followForce;
    private float _maxDistance;
    private float _torqueFactor;
    private LayerMask _raycastLayer;

    public RagdollLimbFollower(Rigidbody limb, float followForce = 50f, float maxDistance = 2f, float torqueFactor = 0.5f, LayerMask? raycastLayer = null)
    {
        _limb = limb;
        _followForce = followForce;
        _maxDistance = maxDistance;
        _torqueFactor = torqueFactor;
        _raycastLayer = raycastLayer ?? ~0; // Default to everything
    }

    public void FollowMouse(Camera camera)
    {
        if (_limb == null || camera == null) return;

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 10f, _raycastLayer))
        {
            Vector3 targetPos = hit.point;
            Vector3 direction = targetPos - _limb.position;

            _limb.AddForce(direction * _followForce, ForceMode.Force);

            if (_torqueFactor > 0f)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction.normalized);
                Quaternion deltaRot = targetRot * Quaternion.Inverse(_limb.rotation);
                deltaRot.ToAngleAxis(out float angle, out Vector3 axis);
                if (angle > 180f) angle -= 360f;
                _limb.AddTorque(axis.normalized * angle * _torqueFactor, ForceMode.Force);
            }

            if (direction.magnitude > _maxDistance)
            {
                Vector3 clampedDir = direction.normalized * _maxDistance;
                _limb.MovePosition(_limb.position + clampedDir * Time.deltaTime);
            }

            Debug.DrawLine(_limb.position, targetPos, Color.red);
        }
    }
}