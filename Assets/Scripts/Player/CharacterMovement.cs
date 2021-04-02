using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    private float moveInput;

    public Transform playerTransform;
    public AudioSource footStep;
    public new Light light;
    //private bool hit;
    private Rigidbody2D rb2d;

    [System.NonSerialized] public bool isGrounded;
    private bool inAir;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Light spotlight;
    Animator animator;
    //[HideInInspector] public bool isAttacking = false;

    [System.NonSerialized] public bool onFightIdle = false;
    public BoxCollider2D parryDetector;
    private bool canParry = false, canAttack = true;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inAir = false;
    }

    private void Update()
    {
        int timeCounter = DNCycle.getCount();

        if (timeCounter >= 6 && timeCounter < 19)
        {
            spotlight.gameObject.SetActive(false);
        }
        else
        {
            spotlight.gameObject.SetActive(true);
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        // Code for jump press space to jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("Jump");
            animator.SetBool("isJumping", true);
            inAir = true;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponent<PlayerStats>().AddItem("Nice");
        }

        // Code for backflip press left alt key to perform
        if (Input.GetKeyDown(KeyCode.LeftAlt) && canAttack)
        {
            animator.SetTrigger("Backflip");
        }

        // Code for punch press left mouse button to perform
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            CancelInvoke("ResetToFightPose");

            onFightIdle = false;
            animator.SetBool("Fight", true);
            animator.SetBool("Jab", true);

            Invoke("ResetToFightPose", 2f);
        }

        // Parry system
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Parry");
        }

        // Check if FightIdle animation is being played so wait 3 seconds before turning to normal idle
        if (onFightIdle)
        {
            CancelInvoke("ResetFightPose");
            onFightIdle = false;
            Invoke("ResetFightPose", 3f);
        }

        // Code for back push press right mouse button to perform
        if (Input.GetKeyDown(KeyCode.LeftControl) && canAttack)
        {
            CancelInvoke("ResetFightPose");
            var rotation = transform.rotation;
            rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y - 180, 0));
            transform.rotation = rotation;
            animator.SetTrigger("BackPush");
            animator.SetBool("Fight", true);

            Invoke("ResetFightPose", 3f);
        }

        // Code for saving press s to save
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameObject.GetComponent<PlayerStats>().SavePlayer();
            GlobalStats.Instance.Save();
        }

        // Code for loading press L to load
        if (Input.GetKeyDown(KeyCode.L))
        {
            GlobalStats.Instance.Load();
        }

    }

    void ResetToFightPose()
    {
        animator.SetBool("Jab", false);
    }

    void ResetFightPose()
    {
        animator.SetBool("Fight", false);
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        
        if (Input.GetAxis("Sprint") != 0 && moveInput != 0 && isGrounded) { rb2d.velocity = new Vector2(moveInput * runSpeed, rb2d.velocity.y); }
        else if (Input.GetAxis("Sprint") == 0 && moveInput != 0) { rb2d.velocity = new Vector2(moveInput * walkSpeed, rb2d.velocity.y); }


        if (moveInput > 0f)
        {
            if (light != null)
            {
                light.transform.position = new Vector3(0f, -6.9f, -38.85001f);
                light.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (moveInput < 0f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 180, 0);
            if (light != null)
            {
                light.transform.position = new Vector3(0f, -6.9f, -11.83f);
                light.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        animator.SetFloat("velocityX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        animator.SetFloat("Run", Mathf.Abs(Input.GetAxisRaw("Sprint")));

    }

    public void PlayFootstep()
    {
        if (isGrounded)
        {
            footStep.Play();
        }
    }
    
    public void JumpStart()
    {
        rb2d.velocity = Vector2.up * jumpForce;
    }

    public void JumpEnd()
    {
        animator.SetBool("isJumping", false);
        inAir = false;
    }

    public void CameraShake()
    {
        var mc = GameObject.Find("Main Camera");
        if (mc.GetComponent<Cinemachine.CinemachineBrain>() == null)
        {
            mc.transform.position = new Vector3(mc.transform.position.x - 0.15f, mc.transform.position.y + 0.1f, mc.transform.position.z);
            Invoke("moveCam", 0.05f);
        }
    }

    void moveCam()
    {
        var mc = GameObject.Find("Main Camera");
        if (mc.GetComponent<Cinemachine.CinemachineBrain>() == null)
        {
            mc.transform.position = new Vector3(mc.transform.position.x + 0.15f, mc.transform.position.y - 0.1f, mc.transform.position.z);

        }
    }

    /// <summary>
    /// Toggles parry true or false.
    /// </summary>
    private void ToggleParry()
    {
        CancelInvoke("ToggleParry");

        // If player can parry then make it false instantly
        if (canParry)
        {
            canParry = false;
            parryDetector.enabled = false;
        }

        // Otherwise toggle canParry true and wait till specified second(s) before toggling it to false
        else
        {
            canParry = true;
            Invoke("ToggleParry", 1f);
            parryDetector.enabled = true;
        }

        ToggleCanAttack();
    }

    /// <summary>
    /// Toggles the canAttack attribute of player, allowing or disallowing player to attack.
    /// </summary>
    private void ToggleCanAttack()
    {
        canAttack = !canAttack;
    }

}
