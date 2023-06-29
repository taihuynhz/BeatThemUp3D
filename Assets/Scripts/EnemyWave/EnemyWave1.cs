using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave1 : MonoBehaviour
{
    [SerializeField] GameObject handPointerControl;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnemySpawner.Instance.SpawnEnemy(Random.Range(15, 20));
            EnemySpawner.Instance.SpawnEnemy(Random.Range(25, 30));
            handPointerControl.SetActive(true);
            Destroy(gameObject);
        }
    }
}
