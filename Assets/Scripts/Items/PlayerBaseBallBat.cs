using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseBallBat : Weapon
{
    protected Image enemyHPBarBorder;
    protected Image enemyHPBar;

    protected void OnDisable()
    {
        if (useTimes == 0)
        {
            // Play BaseballBatBreak sound
            Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/BaseballBatBreak") as AudioClip);
        }
    }

    protected void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        enemyHPBarBorder = GameObject.Find("EnemyHPBarBorder").GetComponent<Image>();
        enemyHPBar = GameObject.Find("EnemyHPBar").GetComponent<Image>();
    }

    protected override void HideWeapon()
    {
        if (useTimes <= 0)
        {
            useTimes = 0;
            player.GetComponent<PlayerController>().weaponEquipped = false;
            player.GetComponent<PlayerController>().baseballBatEquipped = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            useTimes--;
            other.transform.GetComponentInChildren<DamageReceiver>().HPDeduct(5);

            // Play BaseballBatHit sound
            Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/BaseballBatHit") as AudioClip);

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

    protected void ShowEnemyUI()
    {
        Color alpha = Color.white;
        alpha.a = 255f;

        enemyHPBar.color = alpha;
        enemyHPBarBorder.color = alpha;
    }

    protected void HideEnemyUI()
    {
        Color alpha = Color.white;
        alpha.a = 0f;

        enemyHPBar.color = alpha;
        enemyHPBarBorder.color = alpha;
    }

    protected IEnumerator DeactivateUIAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        HideEnemyUI();
    }
}
