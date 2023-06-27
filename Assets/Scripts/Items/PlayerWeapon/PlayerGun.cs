using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Weapon
{
    protected void OnDisable()
    {
        if (useTimes == 0)
        {
            // Play GunEmpty sound
            Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/GunEmpty") as AudioClip);
        }
    }

    protected override void HideWeapon()
    {
        if (useTimes <= 0 && player.GetComponent<PlayerController>().gunEquipped)
        {
            useTimes = 0;
            player.GetComponent<PlayerController>().gunEquipped = false;
            player.GetComponent<PlayerController>().weaponEquipped = false;
        }
    }
}
