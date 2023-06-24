using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenCrateDestroyedSpawner : Spawner
{
    protected static WoodenCrateDestroyedSpawner instance;
    public static WoodenCrateDestroyedSpawner Instance => instance;
    protected Vector3 spawnPos;

    protected void Awake() => CreateSingleton();

    protected override void LoadPrefabs()
    {
        prefab = Resources.Load("Crate/WoodenCrateDestroyed") as GameObject;
    }

    public void SpawnWoodenCrateDestroyed(Transform crate)
    {
        GameObject hitFX = Spawn();

        if (hitFX == null) return;

        spawnPos = new Vector3(crate.position.x, crate.position.y, crate.position.z);
        hitFX.transform.position = spawnPos;
        hitFX.gameObject.SetActive(true);
    }

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
