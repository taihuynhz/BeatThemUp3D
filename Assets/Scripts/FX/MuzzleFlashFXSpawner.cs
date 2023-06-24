using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashFXSpawner : Spawner
{
    protected static MuzzleFlashFXSpawner instance;
    public static MuzzleFlashFXSpawner Instance => instance;
    protected Vector3 spawnPos;
    [SerializeField] protected Vector3 spawnPosOffset;

    protected void Awake() => CreateSingleton();

    protected override void LoadPrefabs()
    {
        prefab = Resources.Load("FX/MuzzleFlash") as GameObject;
    }

    public void SpawnMuzzleFlashFX()
    {
        GameObject muzzleFlashFX = Spawn();
        if (muzzleFlashFX == null) return;

        Transform player = GameObject.Find("Player").transform;
        spawnPos = new Vector3((player.rotation.y > 0 ? player.position.x + spawnPosOffset.x : player.position.x - spawnPosOffset.x), player.position.y + 2.45f + spawnPosOffset.y, player.position.z);
        muzzleFlashFX.transform.position = spawnPos;
        muzzleFlashFX.transform.rotation = Quaternion.Euler(0f, (player.rotation.y > 0 ? 0 : 180f), 0f);
        muzzleFlashFX.gameObject.SetActive(true);
    }

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
