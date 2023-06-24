using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DamageSender : MonoBehaviour
{
    [Header("===== Parameters =====")]
    [SerializeField] float damage = 1;
    [SerializeField] protected LayerMask collisionLayer;
    [SerializeField] protected AnimationClip[] attackClip;
    [SerializeField] protected GameObject[] attackPoint;

    protected float attackPointRadius = 0.8f;

    [SerializeField] bool canShowEnemyUI;
    protected Image enemyHPBarBorder;
    protected Image enemyHPBar;

    protected virtual void Awake()
    {
        Initialize();
        InitAnimationEvent();
    }

    protected virtual void Initialize()
    {
        enemyHPBarBorder = GameObject.Find("EnemyHPBarBorder").GetComponent<Image>();
        enemyHPBar = GameObject.Find("EnemyHPBar").GetComponent<Image>();
    }

    protected virtual void InitAnimationEvent()
    {
        // For override
    }

    protected void SendDamage()
    {
        //foreach (GameObject point in attackPoint)
        for (int i = 0; i < attackPoint.Length; i++)
        {
            Collider[] hits = Physics.OverlapSphere(attackPoint[i].transform.position, attackPointRadius, collisionLayer);

            for (int j = 0; j < hits.Length; j++)
            {
                DamageReceiver damageReceiver = hits[j].gameObject.transform.GetComponentInChildren<DamageReceiver>();

                if (damageReceiver == null) return;
                damageReceiver.HPDeduct(damage);

                if (damageReceiver.isEnemy)
                {
                    // Play hit sound
                    Audio.Instance.AudioSource.PlayOneShot(RandomHitSound(Random.Range(0, 6)));

                    // Show enemy UI
                    if (canShowEnemyUI)
                    {
                        ShowEnemyUI();
                        EnemyUI.Instance.DisplayEnemyHP(damageReceiver.hP);
                    }

                    // Hide enemy UI
                    if (damageReceiver.hP <= 0)
                    {
                        StartCoroutine(DeactivateUIAfterTime(2));
                    }
                }

                if (damageReceiver.isCrate)
                {
                    // Play CrateHit sound
                    Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/CrateHit") as AudioClip);
                }

                if (damageReceiver.isDrumBarrel)
                {
                    // Play DrumBarrelHit sound
                    Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/DrumBarrelHit") as AudioClip);
                }
            }
        }
    }

    protected void SetAnimationEvent(float time, int attackClipIndex)
    {
        AnimationEvent sendDamageEvent = new AnimationEvent();
        sendDamageEvent.time = time;
        sendDamageEvent.functionName = "SendDamage";
        attackClip[attackClipIndex].AddEvent(sendDamageEvent);
    }

    protected AudioClip RandomHitSound(int hit)
    {
        switch (hit)
        {
            case 0: return Resources.Load("Audio/PunchHit1") as AudioClip;
            case 1: return Resources.Load("Audio/PunchHit2") as AudioClip;
            case 2: return Resources.Load("Audio/PunchHit3") as AudioClip;
            case 3: return Resources.Load("Audio/PunchHit4") as AudioClip;
            case 4: return Resources.Load("Audio/PunchHit5") as AudioClip;
            case 5: return Resources.Load("Audio/PunchHit6") as AudioClip;
            default: return null;
        }
    }

    protected void HideEnemyUI()
    {
        Color alpha = Color.white;
        alpha.a = 0f;

        enemyHPBar.color = alpha;
        enemyHPBarBorder.color = alpha;
    }

    protected void ShowEnemyUI()
    {
        Color alpha = Color.white;
        alpha.a = 255f;

        enemyHPBar.color = alpha;
        enemyHPBarBorder.color = alpha;
    }

    protected IEnumerator DeactivateUIAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        HideEnemyUI();
    }
}
