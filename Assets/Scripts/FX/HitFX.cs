using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFX : MonoBehaviour
{
    protected void Update()
    {
        StartCoroutine(DespawnAfterTime(0.5f));
    }

    protected IEnumerator DespawnAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        HitFXSpawner.Instance.Despawn(transform);
    }
}
