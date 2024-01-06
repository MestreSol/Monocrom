using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public float amountToPool;
    public float timeToDestroy;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    public GameObject AddPooledObject()
    {
        GameObject obj = (GameObject)Instantiate(objectToPool);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        Destroy(obj, timeToDestroy);
        return obj;
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}