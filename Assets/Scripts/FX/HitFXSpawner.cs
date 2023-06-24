using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFXSpawner : Spawner
{
    protected static HitFXSpawner instance;
    public static HitFXSpawner Instance => instance;
    protected Vector3 spawnPos;

    protected void Awake() => CreateSingleton();

    protected override void LoadPrefabs()
    {
        prefab = Resources.Load("FX/HitEffect") as GameObject;
    }

    public void SpawnHitFX(Transform enemy)
    {
        GameObject hitFX = Spawn();

        if (hitFX == null) return;

        spawnPos = new Vector3(enemy.position.x, 0, enemy.position.z);
        hitFX.transform.position = spawnPos;
        hitFX.gameObject.SetActive(true);
    }

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
