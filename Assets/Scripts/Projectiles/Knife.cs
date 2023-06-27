using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knife : Projectile
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            other.transform.GetComponentInChildren<DamageReceiver>().HPDeduct(7);
            BulletSpawner.Instance.Despawn(transform);

            // Play KnifeHit sound
            Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/KnifeHit") as AudioClip);

            // Knockdown the enemy
            other.GetComponentInChildren<EnemyAnimation>().KnockDownAnimation();

            // Make enemy fly when get hit
            if (GameObject.Find("Player").transform.rotation.y > 0)
            {
                other.GetComponent<Rigidbody>().AddForce(new Vector3(1500f, 350f, 0), ForceMode.Acceleration);
            }
            else
            {
                other.GetComponent<Rigidbody>().AddForce(new Vector3(-1500f, 350f, 0), ForceMode.Acceleration);
            }

            // Show enemy UI
            ShowEnemyUI();
            EnemyUI.Instance.DisplayEnemyHP(other.transform.GetComponentInChildren<DamageReceiver>().hP);

            // Hide enemy UI
            if (other.transform.GetComponentInChildren<DamageReceiver>().hP <= 0)
            {
                HideEnemyUI();
            }
        }
    }

    protected override IEnumerator DespawnAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        KnifeSpawner.Instance.Despawn(transform);
    }
}
