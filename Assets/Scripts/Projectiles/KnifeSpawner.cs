using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : Spawner
{
    protected static KnifeSpawner instance;
    public static KnifeSpawner Instance => instance;
    protected Vector3 spawnPos;

    protected void Awake() => CreateSingleton();

    protected override void LoadPrefabs()
    {
        prefab = Resources.Load("Projectiles/Knife") as GameObject;
    }

    public void SpawnKnife()
    {
        GameObject knife = Spawn();
        if (knife == null) return;

        Transform player = GameObject.Find("Player").transform;
        spawnPos = new Vector3(player.position.x, player.position.y + 2.45f, player.position.z);
        knife.transform.position = spawnPos;
        knife.transform.rotation = (player.rotation.y > 0 ? Quaternion.Euler(-90f, 0f, 0f) : Quaternion.Euler(-90f, 0f, 180f));
        knife.gameObject.SetActive(true);
    }

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
