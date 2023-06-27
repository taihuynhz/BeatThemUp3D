using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneBaseballBat : PickedUpItem
{
    protected override void PickUp()
    {
        distanceToPlayer = player.position - transform.position;
        if (player.GetComponent<PlayerController>().weaponEquipped == false && pickUpAllowed && Input.GetKeyDown(KeyCode.E) && distanceToPlayer.magnitude <= pickUpRange)
        {
            player.GetComponentInChildren<PlayerAnimation>().PickUpAnimation();
            player.GetComponent<PlayerController>().weaponEquipped = true;

            if (gameObject.CompareTag("BaseballBat"))
            {
                player.GetComponent<PlayerController>().baseballBatEquipped = true;
                player.GetComponent<PlayerController>().playerBaseballBat.GetComponent<PlayerBaseBallBat>().useTimes = 3;
            }

            // Play ButtonPress sound
            Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/ButtonPress") as AudioClip);

            Destroy(gameObject, 0.3f);
        }
    }
}
