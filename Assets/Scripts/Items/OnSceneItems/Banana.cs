using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDamageReceiver.Instance.HPAdd(40);
            other.GetComponentInChildren<PlayerAnimation>().lastHP = other.GetComponentInChildren<PlayerDamageReceiver>().hP;

            // Play ButtonPress sound
            Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/ButtonPress") as AudioClip);

            // Destroy gameObject
            Destroy(gameObject);
        }
    }
}
