using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] public int useTimes = 3;

    protected void Update()
    {
        HideWeapon();
    }

    protected virtual void HideWeapon()
    {
    //    if (useTimes <= 0)
    //    {
    //        useTimes = 0;
    //        player.GetComponent<PlayerController>().weaponEquipped = false;

    //        if (gameObject.CompareTag("BaseballBat"))
    //        {
    //            player.GetComponent<PlayerController>().baseballBatEquipped = false;
    //        }

    //        if (gameObject.CompareTag("Gun"))
    //        {
    //            player.GetComponent<PlayerController>().gunEquipped = false;
    //        }

    //        if (gameObject.CompareTag("Knife"))
    //        {
    //            player.GetComponent<PlayerController>().knifeEquipped = false;
    //        }
    //    }
    }
}



