using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("===== Components =====")]
    [SerializeField] protected new Rigidbody rigidbody;
    [Header("===== Parameters =====")]
    [SerializeField] protected float moveSpeed = 4f;
    [SerializeField] protected float walkSpeed = 4f;
    [SerializeField] protected float runSpeed = 8f;
    [SerializeField] protected float jumpVelocity = 5f;
    [SerializeField] public bool weaponEquipped;
    [SerializeField] public bool gunEquipped;
    [SerializeField] public bool knifeEquipped;
    [SerializeField] public bool baseballBatEquipped;
    [Header("===== Items =====")]
    [SerializeField] public GameObject playerGun;
    [SerializeField] public GameObject playerKnife;
    [SerializeField] public GameObject playerBaseballBat;

    protected float rotation_Y = -90f;
    protected float rotationSpeed = 15f;

    [SerializeField] public bool isRunning;
    [SerializeField] LayerMask groundLayer;

    protected void Update()
    {
        CheckRun();
        Moving();
        Jumping();
    }

    protected void Moving()
    {   
        // Detect movement for the character
        rigidbody.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, rigidbody.velocity.y, Input.GetAxisRaw("Vertical") * moveSpeed);
       
        // Rotate the character when moving
        if (Input.GetAxisRaw("Horizontal") > 0) // Rotate the character a 90 degree angle when moving right
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotation_Y), 0f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) // Rotate the character a -90 degree angle when moving left
        {
            transform.rotation = Quaternion.Euler(0f, rotation_Y, 0f);
        }

        // Stop moving while attacking
        if (transform.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Punch1") ||
            transform.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Punch2") ||
            transform.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Punch3") ||
            transform.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Kick1") ||
            transform.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Kick2") ||
            transform.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FireGun") ||
            transform.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SwingWeapon") ||
            transform.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PickUp") ||
            transform.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Land"))
        {
            rigidbody.velocity = Vector3.zero ;
        }
        else
        {
            moveSpeed = isRunning ? runSpeed : walkSpeed;
        }

        // Player footstep sound
        //if (rigidbody.velocity.x != 0 && IsGrounded() || rigidbody.velocity.z != 0 && IsGrounded())
        //{
        //    if (!OnUpdateAudio.Instance.AudioSource.isPlaying)
        //    {
        //        OnUpdateAudio.Instance.AudioSource.PlayOneShot(RandomFootstepSound(Random.Range(0, 5)));
        //    }
        //}
        //else
        //{
        //    OnUpdateAudio.Instance.AudioSource.Stop();
        //}
    }

    protected void Jumping()
    {
        // Apply jump for the character
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpVelocity, rigidbody.velocity.z);

            // Play player jump sound
            Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/PlayerJump") as AudioClip);
        }

    }

    protected void CheckRun()
    {
        if (transform.GetComponentInChildren<Animator>().GetBool("Run") == true)
        {
            isRunning = true;
            moveSpeed = runSpeed;
        }

        if (transform.GetComponentInChildren<Animator>().GetBool("Run") == false)
        {
            isRunning = false;
            moveSpeed = walkSpeed;
        }
    }

    public virtual bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.1f);
    }

    protected AudioClip RandomFootstepSound(int hit)
    {
        switch (hit)
        {
            case 0: return Resources.Load("Audio/Footstep1") as AudioClip;
            case 1: return Resources.Load("Audio/Footstep2") as AudioClip;
            case 2: return Resources.Load("Audio/Footstep3") as AudioClip;
            case 3: return Resources.Load("Audio/Footstep4") as AudioClip;
            case 4: return Resources.Load("Audio/Footstep5") as AudioClip;
            default: return null;
        }
    }
}
