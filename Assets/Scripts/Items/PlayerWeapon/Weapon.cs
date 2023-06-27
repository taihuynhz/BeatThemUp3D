using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] public int useTimes;

    protected void Update()
    {
        HideWeapon();
    }

    protected virtual void HideWeapon()
    {
        // For override
    }
}



