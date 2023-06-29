using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenCrateDestroyed : MonoBehaviour
{
    protected void Update()
    {
        StartCoroutine(DespawnAfterTime(2));
    }

    protected IEnumerator DespawnAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        WoodenCrateDestroyedSpawner.Instance.Despawn(transform);
    }
}
