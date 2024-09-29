using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    [SerializeField] private GameObject afterImagePrefab;

    private Queue<GameObject> availibleObjects = new Queue<GameObject>();

    public static PlayerAfterImagePool instance {get; private set;}

    private void Awake()
    {
        instance = this;
        GrowPool();
    }
    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availibleObjects.Enqueue(instance);
    }
    private void GrowPool()
    {
        var instanceToAdd = Instantiate(afterImagePrefab);
        instanceToAdd.transform.SetParent(transform);
        AddToPool(instanceToAdd);
    }

    public GameObject GetFromPool()
    {
        if(availibleObjects.Count == 0)
        {
            GrowPool();
        }
        var instance = availibleObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
