using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenCrate : DamageReceiver
{
    protected void OnDestroy()
    {
        WoodenCrateDestroyedSpawner.Instance.SpawnWoodenCrateDestroyed(transform);
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
