using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUpItem : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] protected float pickUpRange;
    [SerializeField] protected bool pickUpAllowed;
    protected Vector3 distanceToPlayer;

    protected void Update()
    {
        PickUp();
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.name.Equals("Player"))
        {   
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = false;
        }
    }

    protected virtual void PickUp()
    {
        //distanceToPlayer = player.position - transform.position;
        //if (player.GetComponent<PlayerController>().weaponEquipped == false && pickUpAllowed && Input.GetKeyDown(KeyCode.E) && distanceToPlayer.magnitude <= pickUpRange)
        //{
        //    player.GetComponentInChildren<PlayerAnimation>().PickUpAnimation();
        //    player.GetComponent<PlayerController>().weaponEquipped = true;

        //    if (gameObject.CompareTag("Gun"))
        //    {
        //        player.GetComponent<PlayerController>().gunEquipped = true;
        //        player.GetComponent<PlayerController>().playerGun.GetComponent<PlayerGun>().useTimes = 3;
        //    }

        //    if (gameObject.CompareTag("BaseballBat"))
        //    {
        //        player.GetComponent<PlayerController>().baseballBatEquipped = true;
        //        player.GetComponent<PlayerController>().playerBaseballBat.GetComponent<PlayerBaseBallBat>().useTimes = 3;
        //    }

        //    if (gameObject.CompareTag("Knife"))
        //    {
        //        player.GetComponent<PlayerController>().knifeEquipped = true;
        //        player.GetComponent<PlayerController>().playerKnife.GetComponent<PlayerKnife>().useTimes = 1;
        //    }

        //    // Play ButtonPress sound
        //    Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/ButtonPress") as AudioClip);

        //    Destroy(gameObject, 0.3f);
        //}
    }
}
