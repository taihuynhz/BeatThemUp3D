using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected int spawnCount = 0;
    [SerializeField] public Transform holder;
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected List<GameObject> pooledObjects = new List<GameObject>();

    protected void Reset()
    {
        LoadComponents();
    }

    protected void LoadComponents()
    {
        LoadHolder();
        LoadPrefabs();
    }

    protected virtual void LoadHolder()
    {
        if (holder != null) return;
        holder = transform.Find("Holder");
    }

    protected virtual void LoadPrefabs()
    {
        // For override
    }

    public virtual GameObject Spawn()
    {
        GameObject newPrefab = GetObjectFromPool(prefab);
        spawnCount++;
        return newPrefab;
    }

    protected GameObject GetObjectFromPool(GameObject prefab)
    {
        foreach (GameObject pooledObject in pooledObjects)
        {
            if (pooledObject == null) continue;
            pooledObjects.Remove(pooledObject);
            return pooledObject;
        }

        GameObject newPrefab = Instantiate(prefab, holder);
        return newPrefab;
    }

    public virtual void Despawn(Transform obj)
    {
        pooledObjects.Add(obj.gameObject);
        obj.gameObject.SetActive(false);
        spawnCount--;
    }
}
