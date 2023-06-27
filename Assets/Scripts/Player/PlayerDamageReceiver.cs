using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    protected static PlayerDamageReceiver instance;
    public static PlayerDamageReceiver Instance => instance;

    protected void Awake() => CreateSingleton();

    public virtual void HPAdd(float amount)
    {
        // Do nothing if the character is dead 
        if (isDead) return;

        // Add HP
        hP += amount;

        // Check if HP = 100
        if (hP >= 100)
        {
            hP = 100;
        }
    }

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
