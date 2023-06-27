using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnife : Weapon
{
    protected override void HideWeapon()
    {
        if (useTimes <= 0 && player.GetComponent<PlayerController>().knifeEquipped)
        {
            useTimes = 0;
            player.GetComponent<PlayerController>().knifeEquipped = false;
            player.GetComponent<PlayerController>().weaponEquipped = false;
        }
    }
}
