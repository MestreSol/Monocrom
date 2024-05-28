using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public List<Transform> backgrounds;
    public float smoothing = 1f;   

    private float[] parallaxScales;
    private Transform cam;
    private Vector3 previousCamPos;

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start(){
        previousCamPos = cam.position;
        parallaxScales = new float[backgrounds.Count];
        for (int i = 0; i < backgrounds.Count; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    void Update(){
        for (int i = 0; i < backgrounds.Count; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        previousCamPos = cam.position;
    }
}
