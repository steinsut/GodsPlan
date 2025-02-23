using System;
using UnityEngine;
using UnityEngine.Audio;

public class playerController : MonoBehaviour
{

    [SerializeField]
    private endTransStateManager EndStateManager;

    public float speed = 5.0f;
    public float jumpStrengthModifier = 1f;
    [SerializeField]
    private Rigidbody2D rigidbody;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Animator spriteAnimationController;

    private BoxCollider2D footColider;

    [SerializeField]
    private AudioSource fallSource;

    [SerializeField]
    private AudioSource walkSource, jumpSource, hurtSource;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        footColider = GetComponent<BoxCollider2D>();
    }

    float horizontalMovement = 0.0f;
    float verticalMovement = 0.0f;
    float jumpForce = 0.0f;

    [SerializeField]
    private float fallSoundThreshhold = 3.0f;

    private const float jumpThreshold = 0.0001f;

    [SerializeField]
    private float gravitySlowdownModifier = 3f;

    private float pressTime = 0.0f;
    [SerializeField]
    private float pressTimeMax = 0.05f;

    [SerializeField]
    private float downwardVelocityTrigger = 0.1f;

    private bool onAir = true;

    [SerializeField]
    private float verticalSpeed = 1f;
    private int ladderCount = 0;

    [SerializeField]
    private bool controllable = true;

    [SerializeField]
    private float deathForce = 4f;

    [SerializeField]
    private GameObject levelParent;

    [SerializeField]
    private float stepGap = 0.1f;
    private float currentStep = 0.0f;

 
    void stopControls()
    {
        controllable = false;
        EndStateManager.setupReturn(false);
        //GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>().ReturnFromMinigame(levelParent, false);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("hazard") || collision.gameObject.CompareTag("deathReason"))
        {
            if (collision.contactCount > 0)
            {
                
                if (controllable)
                {
                    ContactPoint2D contactPoint = collision.GetContact(0);
                    rigidbody.AddForceAtPosition((new Vector2(transform.position.x, transform.position.y) - contactPoint.point ).normalized * deathForce * contactPoint.relativeVelocity ,contactPoint.point);
                    hurtSource.Play();
                }
                stopControls();

            }
        } else
        {
            if (collision.relativeVelocity.magnitude > fallSoundThreshhold)
            {
                fallSource.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( Mathf.Abs(horizontalMovement) > 0.1f)
        {
            spriteRenderer.flipX = horizontalMovement < 0;
        }
        spriteAnimationController.SetBool("touchGrass", !onAir);
        spriteAnimationController.SetBool("sleep", !controllable);

        if (!controllable)
        {
            return;
        }
        
        float verticalInput = Input.GetAxis("Vertical");
        if (ladderCount > 0)
        {
            verticalMovement = verticalInput * verticalSpeed;
        } else
        {
            float jumpInput = Input.GetAxis("Jump");
            if (jumpInput > jumpThreshold)
            {
                if (onAir)
                {
                    pressTime += Time.deltaTime;
                }
                jumpForce = jumpInput * jumpStrengthModifier;
            } else {
                pressTime = 0.0f;
                jumpForce = 0.0f;
            }
        }


        if (rigidbody.linearVelocityY < -downwardVelocityTrigger)
        {
            pressTime -= Time.deltaTime * 2;
            pressTime = Math.Clamp(pressTime, 0.0f, pressTime);
        }

        if (ladderCount > 0)
        {
            rigidbody.gravityScale = 0;
        } else
        {
            rigidbody.gravityScale = 1 - Mathf.Clamp(pressTime - pressTimeMax, 0, pressTimeMax) * gravitySlowdownModifier;
        }


        float horizontalInput = Input.GetAxis("Horizontal");
        horizontalMovement = horizontalInput * speed;

        spriteAnimationController.SetFloat("abs_hi", Mathf.Abs(horizontalMovement));

        if (controllable)
        {
            if (Mathf.Abs(horizontalMovement) > 0.1f && !onAir)
            {
                currentStep += Time.deltaTime;
            } else
            {
                currentStep = 0;
            }
        } else
        {
            currentStep = 0;
        }
        if (currentStep > stepGap)
        {
            walkSource.Play();
            currentStep = 0;
        }
    }

    private void FixedUpdate()
    {

        if (!controllable)
        {
            return;
        }

        rigidbody.linearVelocityX = horizontalMovement;

        if (ladderCount > 0)
        {
            rigidbody.linearVelocityY = verticalMovement;
        } else
        {
            if (footColider.IsTouchingLayers(LayerMask.GetMask("canWalk")))
            {
                
                if (onAir)
                {
                    pressTime = 0;
                }
                onAir = false;
                rigidbody.AddForceY(jumpForce);
                if (jumpForce > 0.1f)
                {

                    jumpSource.Play();
                }
            }
            else
            {
                onAir = true;
            }

        }

    }

    void ladderEnterTrigger()
    {
        // Called through messages when a ladder detects a valid trigger entry by the player
        ladderCount += 1;
    }

    void ladderExitTrigger()
    {
        // Called through msgs when a ladder detects a trigger exit by the player
        ladderCount -= 1;
    }
}
