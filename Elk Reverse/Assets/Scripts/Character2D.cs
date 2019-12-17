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
    private bool jumping = true;
    private float accumulatedNormalizedTime = 0.0f;

    [TagSelector]
    public string wagonTag = "Movable";
    private bool isWagonGrabbed = false;
    private GameObject wagonGrabbed = null;
    private FixedJoint2D wagoncharacterjoint = null;

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
            {
                UnityEngine.Debug.Log(colliders[i].name);
                m_Grounded = true;
                break;
            }
        }
        animator.SetBool("Grounded", m_Grounded);
        animator.SetFloat("SpeedY", rb2d.velocity.y);
        if (m_Grounded && jumping)
        {
            jumping = false;
            animator.SetBool("Jumping", jumping);
        }
    }

    private void LateUpdate()
    {
        if (isWagonGrabbed)
        {
            //var wagonrb2d = wagonGrabbed.GetComponent<Rigidbody2D>();
           // wagonrb2d.MovePosition(transform.Find(""))
        }
    }


    public void Move(float direction)
    {
        animator.SetFloat("SpeedX", Mathf.Abs(direction));
        //Output the current Animation name and length to the screen
        //if (!isWagonGrabbed)
            rb2d.velocity = new Vector2(direction * m_maxSpeed, rb2d.velocity.y);
        /*else
            rb2d.velocity = new Vector2(direction * m_maxWagonSpeed, rb2d.velocity.y);*/


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
        {
            rb2d.AddForce(new Vector2(0, m_JumpForce));
            jumping = true;
            animator.SetBool("Jumping", jumping);
        }
    }

    // Functions for controlling the lantern
    public void EnableLantern()
    {
        UnityEngine.Debug.Log("Enable Lantern");
        usingLantern = true;
        animator.SetBool("Lantern", usingLantern);
    }

    public void DisableLantern()
    {
        usingLantern = false;
        animator.SetBool("Lantern", usingLantern);
    }

    public void GrabWagon()
    {
        var wagons = GameObject.FindGameObjectsWithTag(wagonTag);
        var bodyColliders = GetComponentsInChildren<Collider2D>();

        foreach (var wagon in wagons)
        {
            foreach (var collider in wagon.GetComponentsInChildren<Collider2D>())
            {
                foreach (var bodyCollider in bodyColliders)
                {
                    if (bodyCollider.IsTouching(collider))
                    {
                        isWagonGrabbed = true;
                        wagonGrabbed = wagon;
                        var wagonrb2d = wagonGrabbed.GetComponent<Rigidbody2D>();
                        wagonrb2d.isKinematic = false;

                        wagoncharacterjoint = gameObject.AddComponent<FixedJoint2D>();
                        wagoncharacterjoint.connectedBody = wagonrb2d;
                        wagoncharacterjoint.connectedAnchor = wagonrb2d.transform.position;
                        return;
                    }
                }
            }
        }
    }

    public void ReleaseWagon()
    {
        if (isWagonGrabbed)
        {
            var wagonrb2d = wagonGrabbed.GetComponent<Rigidbody2D>();
            wagonrb2d.isKinematic = true;
            Destroy(wagoncharacterjoint);
            isWagonGrabbed = false;
            wagonGrabbed = null;
        }
    }

    public void SetLanternHeight(int height)
    {
        accumulatedNormalizedTime = GameHelpers.GetDecimalPart(animator.GetCurrentAnimatorStateInfo(0).normalizedTime + accumulatedNormalizedTime);
        animator.SetFloat("CurrentLanternFrameOffset", accumulatedNormalizedTime);
        animator.SetInteger("LookingDirection", height);
    }

    private void OnGUI()
    {

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
}
