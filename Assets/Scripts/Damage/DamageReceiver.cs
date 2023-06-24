using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageReceiver : MonoBehaviour
{
    [Header("===== Parameters =====")]
    [SerializeField] public float hP = 100;

    [SerializeField] public bool isDead;
    [SerializeField] public bool isEnemy;
    [SerializeField] public bool isCrate;
    [SerializeField] public bool isDrumBarrel;

    protected void OnEnable()
    {
        Respawn();
    }

    public virtual void HPDeduct(float deduct)
    {
        // Do nothing if the character is dead 
        if (isDead) return;

        // Deduct HP
        hP -= deduct;

        // Check if HP = 0
        if (hP <= 0)
        {
            isDead = true;
            hP = 0;
        }
    }

    public virtual void Respawn()
    {   
        // Set character isDead boolean to false
        isDead = false;
    }

    public virtual void OnDead()
    {   
        if (isDead == true)
        {
            // Set character layer to default
            transform.parent.gameObject.layer = 0;
            // Destroy character gameObject
            Destroy(transform.parent.gameObject, 3f);
        }
    }
}
