using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector2 minLimits;
    public Vector2 maxLimits;
    public float idleTimeThreshold = 3f; // Tempo de inatividade em segundos
    public float idleMoveDistance = 1f; // Distância de movimento durante a inatividade
    private float idleTimer = 0f;
    private Vector3 lastTargetPosition;
    public Vector3 NewOffset = new Vector3(0, 0, -10);
    public float CameraSize;
    public bool targetIdle;
    void Start()
    {
        lastTargetPosition = target.position;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Verificar se a posição suavizada está dentro dos limites permitidos
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minLimits.x, maxLimits.x);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minLimits.y, maxLimits.y);

        // Verificar se o alvo está parado
        bool isTargetPositionSame = target.position == lastTargetPosition;
        targetIdle = isTargetPositionSame && idleTimer >= idleTimeThreshold;

        if (targetIdle)
        {
            idleTimer += Time.deltaTime;
            // Calcular a direção em que o alvo está virado
            Vector3 targetDirection = NewOffset;
            // Mover a câmera na direção do alvo
            smoothedPosition += targetDirection * idleMoveDistance;
        }
        else
        {
            idleTimer = isTargetPositionSame ? idleTimer + Time.deltaTime : 0f;
        }

        transform.position = smoothedPosition;
        lastTargetPosition = target.position;
    }

}