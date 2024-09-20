using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    public float heightOffset = 1.5f;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset + Vector3.up * heightOffset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Vector3 lookAtPosition = target.position + Vector3.up * heightOffset;
        transform.LookAt(lookAtPosition);
    }
}
