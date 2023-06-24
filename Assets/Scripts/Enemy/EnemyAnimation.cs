using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [Header("===== Components =====")]
    [SerializeField] protected Animator animator;

    [HideInInspector] public Transform target;
    [HideInInspector] public float currentAttackTime;
    [HideInInspector] public float defaultAttackTime = 2f;

    protected float chasePlayerAfterAttack = 1f;
    protected float lastHP;

    //protected float standUpTimer = 2f;

    protected void Awake()
    {
        Initialize();
    }

    protected void Update()
    {
        WalkAnimation();
        AttackAnimation();
        HitAnimation();
        DeathAnimation();
    }

    protected void Initialize()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentAttackTime = defaultAttackTime;
        lastHP = transform.GetComponent<EnemyDamageReceiver>().hP;
    }

    protected void WalkAnimation()
    {
        //if (transform.parent.GetComponent<EnemyController>().chasePlayer == false) return;

        //if (Vector3.Distance(transform.position, target.position) > transform.parent.GetComponent<EnemyController>().canAttackDistance)
        //{
        //    if (transform.parent.GetComponent<Rigidbody>().velocity.sqrMagnitude != 0)
        //    {
        //        animator.SetBool("Movement", true);
        //    }
        //}
        //else if (Vector3.Distance(transform.position, target.position) <= transform.parent.GetComponent<EnemyController>().canAttackDistance)
        //{
        //    animator.SetBool("Movement", false);
        //}
    }

    protected void AttackAnimation()
    {
        if (!transform.parent.GetComponent<EnemyController>().attackPlayer) return;

        currentAttackTime += Time.deltaTime;

        if (currentAttackTime > defaultAttackTime)
        {
            RandomAttackAnimation(Random.Range(0, 3));
            currentAttackTime = 0f;
        }

        if (Vector3.Distance(transform.position, target.position) > transform.parent.GetComponent<EnemyController>().canAttackDistance + chasePlayerAfterAttack)
        {
            transform.parent.GetComponent<EnemyController>().attackPlayer = false;
            transform.parent.GetComponent<EnemyController>().chasePlayer = true;
        }
    }

    protected void HitAnimation()
    {
        if (lastHP != transform.GetComponent<EnemyDamageReceiver>().hP)
        {
            RandomHitAnimation(Random.Range(0, 2));
            lastHP = transform.GetComponent<EnemyDamageReceiver>().hP;

            // Spawn hit effect
            HitFXSpawner.Instance.SpawnHitFX(transform.parent);
        }
    }

    protected void DeathAnimation()
    {
        if (transform.GetComponent<EnemyDamageReceiver>().isDead == true)
        {
            animator.SetTrigger("Death");
            transform.parent.GetComponent<EnemyController>().enabled = false;
            transform.parent.GetComponent<EnemyController>().attackPlayer = false;
            transform.parent.GetComponent<EnemyController>().chasePlayer = false;

        //    if (!OnUpdateAudio.Instance.AudioSource.isPlaying)
        //    {
        //        OnUpdateAudio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/EnemyDeath") as AudioClip);
        //    }
        //}
        //else
        //{
        //    OnUpdateAudio.Instance.AudioSource.Stop();
        }
    }

    protected void RandomAttackAnimation(int attack)
    {
        switch (attack)
        {
            case 0: animator.SetTrigger("Attack1"); break;
            case 1: animator.SetTrigger("Attack2"); break;
            case 2: animator.SetTrigger("Attack3"); break;
            default: break;
        }
    }

    protected void RandomHitAnimation(int attack)
    {
        switch (attack)
        {
            case 0: animator.SetTrigger("Hit1"); break;
            case 1: animator.SetTrigger("Hit2"); break;
            default: break;
        }
    }

    //protected void KnockDownAnimation()
    //{
    //    if (EnemyDamageReceiver.Instance.isDead)
    //    {
    //        animator.SetTrigger("KnockDown");
    //        transform.GetComponent<EnemyController>().enabled = false;
    //        Destroy(gameObject, 2f);
    //    }
    //}

    //protected void StandUpAnimation ()
    //{
    //    StartCoroutine("StandUpAfterTime");
    //}

    //IEnumerable StandUpAfterTime()
    //{
    //    yield return new WaitForSeconds(standUpTimer);
    //    animator.SetTrigger("StandUp");
    //}
}
