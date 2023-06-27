using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Spawner
{
    protected static ItemSpawner instance;
    public static ItemSpawner Instance => instance;
    protected Vector3 spawnPos;

    protected void Awake() => CreateSingleton();

    protected override void LoadPrefabs()
    {
        prefab = Resources.Load("Projectiles/Bullet") as GameObject;
    }

    public void SpawnItem(Transform crate)
    {
        GameObject item = Spawn();
        if (item == null) return;

        item.transform.position = crate.position;
        item.gameObject.SetActive(true);
    }

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
