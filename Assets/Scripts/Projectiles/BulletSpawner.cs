using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    protected static BulletSpawner instance;
    public static BulletSpawner Instance => instance;
    protected Vector3 spawnPos;

    protected void Awake() => CreateSingleton();

    protected override void LoadPrefabs()
    {
        prefab = Resources.Load("Projectiles/Bullet") as GameObject;
    }

    public void SpawnBullet()
    {
        GameObject bullet = Spawn();
        if (bullet == null) return;

        Transform player = GameObject.Find("Player").transform;
        spawnPos = new Vector3(player.position.x, player.position.y + 2.45f, player.position.z);
        bullet.transform.position = spawnPos;
        bullet.transform.rotation = (player.rotation.y > 0 ? Quaternion.Euler(-90f, 0f, 0f) : Quaternion.Euler(-90f, 0f, 180f));
        bullet.gameObject.SetActive(true);
    }

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
