using System.Collections.Generic;
using UnityEngine;

// Entity base class
public abstract class Entity : MonoBehaviour
{
    // List of components attached to this entity
    protected List<Component> components = new List<Component>();

    // Add a component to the entity
    public T AddComponent<T>() where T : Component
    {
        T component = gameObject.AddComponent<T>();
        components.Add(component);
        return component;
    }

    // Remove a component from the entity
    public void RemoveComponent<T>() where T : Component
    {
        T component = GetComponent<T>();
        if (component != null)
        {
            components.Remove(component);
            Destroy(component);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        foreach (Component component in components)
        {
            component.Update();
        }
    }
}