using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController2D : MonoBehaviour
{



    [SerializeField]
    private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField]
    [Range(1, 2)]
    private float m_JumpSpeedMultiplier = 1.5f;
    [SerializeField]
    private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField]
    private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    private bool m_Jump;

    [Header("gameplay options")]
    public bool useExternalController = false;
    [SerializeField]
    private DeviceManager.DeviceType deviceType;

    double flowRate;

    [SerializeField]
    [Tooltip("The amount which the player needs to blow in order to jump")]
    private double blowthreshold = 10f;
    public bool inBlowZone;

    private void Awake()
    {
        if (useExternalController)
        {
            //setting up the controller
            DeviceManager.Instance.SetDeviceType(deviceType);
        }

        // Setting up references.
        m_GroundCheck = transform.GetChild(0); //transform.Find("GroundCheck");
        m_CeilingCheck = transform.GetChild(1);  //transform.Find("CeilingCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            if (useExternalController)
            {
                flowRate = DeviceManager.Instance.FlowLMin;

                flowRate = System.Math.Round(flowRate, 1);
                if (deviceType == DeviceManager.DeviceType.KUEFFNER)
                {
                    flowRate *= -1;
                }
            }

            if (CrossPlatformInputManager.GetButton("Jump"))
            {
                flowRate = 199;
            }
            else
            {
                flowRate = 0;
            }

            if (m_Grounded && flowRate >= blowthreshold)
            {
                m_Jump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        m_Anim.SetBool("Ground", m_Grounded);

        // Set the vertical animation
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

        Move(1, m_Jump);
        m_Jump = false;
    }


    public void Move(float move, bool jump)
    {



        // The Speed animator parameter is set to the absolute value of the horizontal input.
        m_Anim.SetFloat("Speed", Mathf.Abs(move));

        // Move the character
        //if (inBlowZone)
        //{

        //}
        /*else*/ if (m_Grounded)
        {
            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
        }
        else
        {
            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed * m_JumpSpeedMultiplier, m_Rigidbody2D.velocity.y);
        }

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        //}
        // If the player should jump...
        if (m_Grounded && jump && m_Anim.GetBool("Ground"))
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}

