using FMOD;
using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2D : MonoBehaviour
{ 
    public float m_maxSpeed = 8f;
    public float m_JumpForce = 400f;
    public LayerMask m_WhatIsGround; // A mask determining what is ground to the character
    public float m_LanternUpLowerBound = 0.1f;
    public float m_LanternDownUpperBound = -0.1f;

    [EventRef]
    public string stepSoundEvent = "";
    private EventInstance stepSoundInstance;
    private const float stepSoundVolume = 0.15f;
    [EventRef]
    public string jumpSoundEvent = "";

    private Animator animator;
    private Rigidbody2D rb2d;
    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f;

    private bool m_Grounded;
    private bool facingLeft = true;
    private bool usingLantern = false;
    private float lanternYOrigin;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        m_GroundCheck = transform.Find("GroundCheck");
	}
	
	// Update is called once per frame
	void Update () {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        animator.SetBool("Grounded", m_Grounded);
        animator.SetFloat("SpeedY", rb2d.velocity.y);
        var time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime - Mathf.Floor(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        animator.SetFloat("CurrentLanternFrameOffset", time);
    }
    

    public void Move(float direction)
    {
        animator.SetFloat("SpeedX", Mathf.Abs(direction));
        //Output the current Animation name and length to the screen
        rb2d.velocity = new Vector2(direction * m_maxSpeed, rb2d.velocity.y);

        if (facingLeft && direction > 0)
        {
            facingLeft = !facingLeft;
        }
        else if (!facingLeft && direction < 0)
        {
            facingLeft = !facingLeft;
        }

        animator.SetBool("FacingLeft", facingLeft);

    }

    public void Jump()
    {
        if (m_Grounded)
            rb2d.AddForce(new Vector2(0, m_JumpForce));
    }

    // Functions for controlling the lantern
    public void LanternRight()
    {
        if (!facingLeft)
        {
            UnityEngine.Debug.Log("right");
            usingLantern = true;
            animator.SetBool("Lantern", usingLantern);
        }
    }
    public void LanternRightUp()
    {
        if (!facingLeft)
        {
            UnityEngine.Debug.Log("not right");
            usingLantern = false;
            animator.SetBool("Lantern", usingLantern);
        }
    }

    public void LanternLeft()
    {
        if (facingLeft)
        {
            UnityEngine.Debug.Log("left");
            usingLantern = true;
            animator.SetBool("Lantern", usingLantern);
        }
    }

    public void LanternLeftUp()
    {
        if (!facingLeft)
        {
            UnityEngine.Debug.Log("not left");
            usingLantern = false;
            animator.SetBool("Lantern", usingLantern);
        }
    }

    public void LanternHeight(int height)
    {
        UnityEngine.Debug.Log(height);
        animator.SetInteger("LookingDirection", height);
    }

    private void OnGUI()
    {
        /*print("SpeedX: " + rb2d.velocity);
        print("FacingLeft: " + facingLeft);*/
    }

    public void Flip()
    {

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void OnAnimationStep(int frame)
    {
        if (stepSoundInstance.isValid())
        {
            stepSoundInstance.release();
            stepSoundInstance.clearHandle();
        }

        if (!stepSoundInstance.isValid())
        {
            stepSoundInstance = RuntimeManager.CreateInstance(stepSoundEvent);
            stepSoundInstance.setVolume(stepSoundVolume);
            stepSoundInstance.start();
        }
    }

    public void getCurrentLookingDirection()
    {

    }
}
