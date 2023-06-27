using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("===== Components =====")]
    [SerializeField] protected Animator animator;
    protected bool canJumpKick = true;
    
    protected enum AttackState
    {
        None,
        Punch1,
        Punch2,
        Punch3,
        Kick1,
        Kick2,
    }

    protected bool activeTimerReset;
    protected float defaultComboTimer = 0.4f;
    protected float currentComboTimer;
    public float lastHP;

    protected float weaponTimer = 0f;
    protected float weaponDelay = 0.5f;

    protected AttackState currentAttackState;

    protected void Awake()
    {
        Initialize();
    }

    protected void Update()
    {
        MoveAnimation();
        AttackAnimation();
        HitAnimation();
        DeathAnimation();
        JumpAnimation();
        WeaponAnimation();
    }

    protected void Initialize()
    {
        currentComboTimer = defaultComboTimer;
        currentAttackState = AttackState.None;
        lastHP = transform.GetComponent<PlayerDamageReceiver>().hP;
    }

    protected void MoveAnimation()
    {
        // Apply walk animation
        if (Input.GetAxisRaw("Horizontal") != 0|| Input.GetAxisRaw("Vertical") != 0 && !Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("Movement", true);
        }
        else
        {
            animator.SetBool("Movement", false);
        }

        // Apply run animation
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("Run", true);
        }

        if (transform.parent.GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
            animator.SetBool("Run", false);
        }
    }

    protected void AttackAnimation()
    {
        if (transform.parent.GetComponent<PlayerController>().IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                // Stop changing state at the final combo state
                if (currentAttackState == AttackState.Punch3 || currentAttackState == AttackState.Kick1 || currentAttackState == AttackState.Kick2) return;

                // Change to the next combo state
                currentAttackState++;
                activeTimerReset = true;
                currentComboTimer = defaultComboTimer;

                // Trigger punch animations
                switch (currentAttackState)
                {
                    case AttackState.Punch1: animator.SetTrigger("Punch1"); break;
                    case AttackState.Punch2: animator.SetTrigger("Punch2"); break;
                    case AttackState.Punch3: animator.SetTrigger("Punch3"); break;
                    default: break;
                }

                // Play whoosh sound
                Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/Whoosh") as AudioClip);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                // Stop changing state at the final combo state
                if (currentAttackState == AttackState.Kick2 || currentAttackState == AttackState.Punch3) return;

                // Change to the next combo state
                if (currentAttackState == AttackState.None || currentAttackState == AttackState.Punch1 || currentAttackState == AttackState.Punch2)
                {
                    currentAttackState = AttackState.Kick1;
                }
                else if (currentAttackState == AttackState.Kick1)
                {
                    currentAttackState++;
                }

                activeTimerReset = true;
                currentComboTimer = defaultComboTimer;

                // Trigger kick animations
                switch (currentAttackState)
                {
                    case AttackState.Kick1: animator.SetTrigger("Kick1"); break;
                    case AttackState.Kick2: animator.SetTrigger("Kick2"); break;
                    default: break;
                }

                // Play whoosh sound
                Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/Whoosh") as AudioClip);
            }

            // Reset timer for combo attack
            if (activeTimerReset)
            {
                currentComboTimer -= Time.deltaTime;
                if (currentComboTimer <= 0f)
                {
                    currentAttackState = AttackState.None;
                    activeTimerReset = false;
                    currentComboTimer = defaultComboTimer;
                }
            }
        }    
    }

    protected void WeaponAnimation()
    {
        // Handle gunshot animation
        if (transform.parent.GetComponent<PlayerController>().gunEquipped && transform.parent.GetComponent<PlayerController>().playerGun.GetComponent<Weapon>().useTimes > 0) 
        {
            weaponTimer += Time.deltaTime;
            if (weaponTimer < weaponDelay) return;

            if (Input.GetKeyDown(KeyCode.I) && transform.parent.GetComponent<PlayerController>().IsGrounded())
            {
                weaponTimer = 0;
                transform.parent.GetComponent<PlayerController>().playerGun.GetComponent<PlayerGun>().useTimes--;
                transform.parent.GetComponent<PlayerController>().playerGun.SetActive(true);
                animator.SetTrigger("FireGun");

                // Play Gunshot sound
                Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/Gunshot") as AudioClip);

                // Hide gun after time
                StartCoroutine(HideGunAfterTime(0.5f));
            }
        }

        // Handle baseballbat swing animation 
        if (transform.parent.GetComponent<PlayerController>().baseballBatEquipped && transform.parent.GetComponent<PlayerController>().playerBaseballBat.GetComponent<Weapon>().useTimes > 0)
        {
            weaponTimer += Time.deltaTime;
            if (weaponTimer < weaponDelay) return;

            if (Input.GetKeyDown(KeyCode.I) && transform.parent.GetComponent<PlayerController>().IsGrounded())
            {
                weaponTimer = 0;
                transform.parent.GetComponent<PlayerController>().playerBaseballBat.SetActive(true);
                animator.SetTrigger("SwingWeapon");

                // Play whoosh sound
                Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/Whoosh") as AudioClip);

                // Hide baseballbat after time
                StartCoroutine(HideBaseballBatAfterTime(0.5f));
            }
        }

        // Handle throw knife animation 
        if (transform.parent.GetComponent<PlayerController>().knifeEquipped && transform.parent.GetComponent<PlayerController>().playerKnife.GetComponent<Weapon>().useTimes > 0)
        {
            if (Input.GetKeyDown(KeyCode.I) && transform.parent.GetComponent<PlayerController>().IsGrounded())
            {
                transform.parent.GetComponent<PlayerController>().playerKnife.GetComponent<PlayerKnife>().useTimes--;
                transform.parent.GetComponent<PlayerController>().playerKnife.SetActive(true);
                animator.SetTrigger("ThrowWeapon");

                // Play whoosh sound
                Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/Whoosh") as AudioClip);

                // Hide knife after time
                StartCoroutine(HideKnifeBatAfterTime(0.5f));
            }
        }
    }

    public void PickUpAnimation()
    {
        animator.SetTrigger("PickUp");
    }

    protected void HitAnimation()
    {
        if (transform.GetComponent<PlayerDamageReceiver>().hP < lastHP)
        {
            animator.SetTrigger("Hit");
            lastHP = transform.GetComponent<PlayerDamageReceiver>().hP;

            // Play hurt sound
            Audio.Instance.AudioSource.PlayOneShot(RandomHurtSound(Random.Range(0, 4)));
        }
    }

    protected void DeathAnimation()
    {
        if (transform.GetComponent<PlayerDamageReceiver>().isDead == true)
        {
            animator.SetTrigger("Death");
            transform.parent.GetComponent<PlayerController>().enabled = false;
            GetComponent<PlayerAnimation>().enabled = false;
        }
    }

    protected void JumpAnimation()
    {   
        // Handle jumping animation
        if (Input.GetButtonDown("Jump") && transform.parent.GetComponent<PlayerController>().IsGrounded())
        {
            animator.SetTrigger("Jump");
        }
        // Handle falling animation
        if (transform.parent.GetComponent<Rigidbody>().velocity.y < 0 && !transform.parent.GetComponent<PlayerController>().IsGrounded())
        {
            animator.SetBool("Falling", true);
        }
        // Handle landing animation
        else if (transform.parent.GetComponent<PlayerController>().IsGrounded())
        {
            animator.SetBool("Falling", false);
        }

        // Play jump kick animation
        if (transform.parent.GetComponent<Rigidbody>().velocity.y != 0 && !transform.parent.GetComponent<PlayerController>().IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.J) && canJumpKick)
            {
                canJumpKick = false;
                animator.SetTrigger("JumpKick");

                // Play whoosh sound
                Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/Whoosh") as AudioClip);
            }
        }
        else canJumpKick = true;
    }

    protected AudioClip RandomHurtSound(int hit)
    {
        switch (hit)
        {
            case 0: return Resources.Load("Audio/PlayerHurt1") as AudioClip;
            case 1: return Resources.Load("Audio/PlayerHurt2") as AudioClip;
            case 2: return Resources.Load("Audio/PlayerHurt3") as AudioClip;
            case 3: return Resources.Load("Audio/PlayerHurt4") as AudioClip;
            default: return null;
        }
    }

    protected IEnumerator HideGunAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        transform.parent.GetComponent<PlayerController>().playerGun.SetActive(false);
    }
    protected IEnumerator HideBaseballBatAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        transform.parent.GetComponent<PlayerController>().playerBaseballBat.SetActive(false);
    }

    protected IEnumerator HideKnifeBatAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        transform.parent.GetComponent<PlayerController>().playerKnife.SetActive(false);
    }
}
