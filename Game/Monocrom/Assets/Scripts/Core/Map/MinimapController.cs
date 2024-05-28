using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    private bool isDragging = false;
    private bool isZooming = false;
    public bool isActive = false;
    private Vector3 dragStartPosition;
    private Vector3 dragOffset;
    public GameObject minimap;
    public GameObject camera;
    public float speed = 0.1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        if (isDragging)
        {
            dragOffset = Input.mousePosition - dragStartPosition;
            dragStartPosition = Input.mousePosition;
            minimap.transform.position += new Vector3(-dragOffset.x, -dragOffset.y, 0) * speed;
        }
        if (Input.GetMouseButtonDown(1))
        {
            isZooming = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isZooming = false;
        }
        if (isZooming)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                minimap.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                minimap.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isActive)
            {
                minimap.SetActive(false);
                isActive = false;
            }
            else
            {
                minimap.SetActive(true);
                isActive = true;
            }
        }
    }
}
