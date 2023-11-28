using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseHover : MonoBehaviour
{
    void OnMouseEnter()
    {
        Debug.Log("Mouse is over GameObject.");
    }
    public void OnMouseExit()
    {
        Debug.Log("Mouse is no longer on GameObject.");
    }   
}
