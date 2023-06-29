using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPointerControl : MonoBehaviour
{
    [SerializeField] GameObject handPointer;

    protected void Update()
    {
        // Show HandPointer
        if (EnemySpawner.Instance.spawnCount == 0)
        {
            if (handPointer.activeInHierarchy == false)
            {
                StartCoroutine(HandPointFlickering());
            }
        }
    }

    protected IEnumerator HandPointFlickering()
    {   
        yield return new WaitForSeconds(0.5f);
        handPointer.SetActive(true);
    }
}
