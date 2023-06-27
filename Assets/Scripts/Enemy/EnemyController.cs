using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("===== Components =====")]
    [SerializeField] protected new Rigidbody rigidbody;
    [Header("===== Parameters =====")]
    [SerializeField] protected float moveSpeed = 4f;
    [Header("===== Target =====")]
    [SerializeField] public Transform target;

    public float canAttackDistance = 2f;
    public bool chasePlayer;
    public bool attackPlayer;

    protected void Awake()
    {
        Initialize();
        SetChasePlayerToTrue();
    }

    protected void FixedUpdate()
    {       
        ChasePlayer();
        StopAttack();
    }

    protected void Initialize()
    {
        // Ignore collision
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").transform.GetComponent<CapsuleCollider>(), transform.GetComponent<CapsuleCollider>());
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Enemy").transform.GetComponent<CapsuleCollider>(), transform.GetComponent<CapsuleCollider>());

        // Set player target
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected void ChasePlayer()
    {   
        // If enemy isn't chasing player, do nothing
        if (!chasePlayer) return;

        // Check if can chase player
        if (Vector3.Distance(transform.position, target.position) > canAttackDistance)
        {   
            transform.LookAt(target);
            rigidbody.velocity = transform.forward * moveSpeed;

            if (rigidbody.velocity.sqrMagnitude != 0)
            {
                transform.GetComponentInChildren<Animator>().SetBool("Movement", true);
            }
        }
        else if (Vector3.Distance(transform.position, target.position) <= canAttackDistance)
        {
            rigidbody.velocity = Vector3.zero;
            transform.GetComponentInChildren<Animator>().SetBool("Movement", false);
            chasePlayer = false;
            attackPlayer = true;
        }

        // Rotate enemy toward player
        Vector3 direction = (target.position - transform.position).normalized;
        float atan2 = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0f, atan2 * Mathf.Rad2Deg + 90f, 0f);
        
        // Make enemy stay on the ground
        if (rigidbody.velocity.y > 0)
        {
            rigidbody.AddForce(new Vector3(0f, -100f, 0f), ForceMode.Force);
        }
    }

    protected void SetChasePlayerToTrue()
    {
        chasePlayer = true;
    }

    protected void StopAttack()
    {
        // If the player is dead, stop attacking 
        if (GameObject.FindGameObjectWithTag("Player").transform.GetComponentInChildren<PlayerDamageReceiver>().isDead)
        {
            attackPlayer = false;
        }
    }

    public virtual bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.1f);
    }
}
