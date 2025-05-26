using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _target;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 5f;

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
    }

    void LateUpdate()
    {
        if (_target == null) return;

        Vector3 desiredPosition = _target.position + _target.TransformDirection(offset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(_target);
    }
}
