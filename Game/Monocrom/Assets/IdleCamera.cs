using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCamera : MonoBehaviour
{
    public float targetSize = 5f; // Tamanho desejado da câmera
    public float resizeSpeed = 2f; // Velocidade de ajuste do tamanho
    public float defaultSize = 3f; // Tamanho padrão da câmera
    private Camera cameraComponent;
    private float currentSize;

    void Start()
    {
        cameraComponent = GetComponent<Camera>();
        currentSize = cameraComponent.orthographicSize;
    }

    void Update()
    {
        if (gameObject.GetComponent<CameraFollow>().targetIdle)
        {
        // Interpolar suavemente entre o tamanho atual e o tamanho desejado
        currentSize = Mathf.Lerp(currentSize, targetSize, resizeSpeed * Time.deltaTime);

        // Definir o tamanho da câmera
        cameraComponent.orthographicSize = currentSize;
        }
        else{
            currentSize = Mathf.Lerp(currentSize, defaultSize, resizeSpeed * Time.deltaTime);
            cameraComponent.orthographicSize = currentSize;
        }
    }
}