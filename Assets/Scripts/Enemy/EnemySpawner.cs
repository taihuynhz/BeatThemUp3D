using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class EnemySpawner : Spawner
{
    protected static EnemySpawner instance;
    public static EnemySpawner Instance => instance;

    [SerializeField] public int enemyAmount = 3;
    protected Vector3 spawnOffset;

    protected void Awake() => CreateSingleton();

    //protected void Update()
    //{
    //    SpawnEnemy();
    //}

    protected override void LoadPrefabs()
    {
        prefab = Resources.Load("Prefabs/Enemy") as GameObject;
    }

    public void SpawnEnemy()
    {
        //if (spawnCount >= enemyAmount) return;
        GameObject enemy = Spawn();

        if (enemy == null) return;
        spawnOffset = new Vector3(Random.Range(16, 20) + GameObject.FindGameObjectWithTag("Player").transform.position.x, -2.7f, -26.5f);
        enemy.transform.position = transform.position + spawnOffset;
        enemy.gameObject.SetActive(true);
    }

    public void SpawnEnemy(float position)
    {
        if (spawnCount >= enemyAmount) return;
        GameObject enemy = Spawn();

        if (enemy == null) return;
        spawnOffset = new Vector3(position + GameObject.FindGameObjectWithTag("Player").transform.position.x, -2.7f, -26.5f);
        enemy.transform.position = transform.position + spawnOffset;
        enemy.gameObject.SetActive(true);
    }

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
