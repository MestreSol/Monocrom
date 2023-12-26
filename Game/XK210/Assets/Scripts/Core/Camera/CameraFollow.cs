using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector2 minLimits;
    public Vector2 maxLimits;

    public float idleTimeThreshold = 3f;
    public float idleMoveDistance = 1f;
    public float idleTime = 0f;
    private float idleTimer = 0f;

    public Vector3 lastTargetPosition;
    public Vector3 newOffset = new Vector3(0, 0, -10);
    
    public float cameraSize;
    public bool targetIdle;

    private void Start()
    {
        lastTargetPosition = target.position;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minLimits.x, maxLimits.x);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minLimits.y, maxLimits.y);

        if(target.position == lastTargetPosition)
        {
            idleTime += Time.deltaTime;

            if(idleTime >= idleTimeThreshold)
            {
                Vector3 targetDirection = newOffset;
                smoothedPosition += targetDirection * idleMoveDistance;
                targetIdle = true;
            }
        }
        else
        {
            idleTimer = 0f;
            targetIdle = false;
        }
        transform.position = smoothedPosition;
        lastTargetPosition = target.position;
    }
}