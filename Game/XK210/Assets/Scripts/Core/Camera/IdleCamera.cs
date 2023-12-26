using UnityEngine;

public class IdleCamera : MonoBehaviour
{
    public float targetSize = 5f;
    public float resizeSpeed = 2f;
    public float defaultSize = 3f;
    private Camera cam;
    public float currentSize;

    private void Start()
    {
        cam = GetComponent<Camera>();
        currentSize = cam.orthographicSize;
    }
    private void Update()
    {
        if (gameObject.GetComponent<CameraFollow>().targetIdle && GameManager.instance.curState == GameState.Playing)
        {
            // Interpolar suavemente entre o tamanho atual e o tamanho desejado
            currentSize = Mathf.Lerp(currentSize, targetSize, resizeSpeed * Time.deltaTime);

            // Definir o tamanho da câmera
            cam.orthographicSize = currentSize;
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, defaultSize, resizeSpeed * Time.deltaTime);
            cam.orthographicSize = currentSize;
        }
        if (GameManager.instance.curState == GameState.Dialog)
        {
            currentSize = defaultSize;
            cam.orthographicSize = currentSize;
        }
    }
}