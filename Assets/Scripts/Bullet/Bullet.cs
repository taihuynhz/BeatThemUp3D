using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    protected float flySpeed = 50f;
    protected Image enemyHPBarBorder;
    protected Image enemyHPBar;

    protected void Start()
    {
        Initialize();
    }
    protected void Update()
    {
        Fly();
        StartCoroutine(DespawnAfterTime(3));
    }

    protected void Initialize()
    {
        enemyHPBarBorder = GameObject.Find("EnemyHPBarBorder").GetComponent<Image>();
        enemyHPBar = GameObject.Find("EnemyHPBar").GetComponent<Image>();
    }

    protected void Fly()
    {
        Vector3 direction = -transform.right;
        Transform player = GameObject.Find("Player").transform;
        transform.Translate((player.rotation.y > 0 ? direction : -direction) * flySpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            other.transform.GetComponentInChildren<DamageReceiver>().HPDeduct(4);
            BulletSpawner.Instance.Despawn(transform);

            // Play BulletHit sound
            Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/BulletHit") as AudioClip);

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

    protected IEnumerator DespawnAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        BulletSpawner.Instance.Despawn(transform);
    }
}
