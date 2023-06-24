using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Weapon
{
    protected override void HideWeapon()
    {
        if (useTimes <= 0)
        {
            useTimes = 0;
            player.GetComponent<PlayerController>().weaponEquipped = false;
            player.GetComponent<PlayerController>().gunEquipped = false;
        }
    }
}
