using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenCrate : DamageReceiver
{
    [SerializeField] GameObject item;

    protected void OnDestroy()
    {
        if (WoodenCrateDestroyedSpawner.Instance == null) return;
        WoodenCrateDestroyedSpawner.Instance.SpawnWoodenCrateDestroyed(transform);
        item.SetActive(true);
    }

    protected void Update()
    {
        OnDead();
    }

    public override void OnDead()
    {
        if (isDead == true)
        {
            // Set character layer to default
            transform.gameObject.layer = 0;
            // Destroy character gameObject
            Destroy(transform.gameObject);
        }
    }
}
