using System;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpStrengthModifier = 1f;
    [SerializeField]
    private Rigidbody2D rigidbody;

    private BoxCollider2D footColider;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        footColider = GetComponent<BoxCollider2D>();
    }

    float horizontalMovement = 0.0f;
    float verticalMovement = 0.0f;

    private const float jumpThreshold = 0.0001f;

    [SerializeField]
    private float gravitySlowdownModifier = 3f;

    private float pressTime = 0.0f;
    [SerializeField]
    private float pressTimeMax = 0.05f;

    [SerializeField]
    private float downwardVelocityTrigger = 0.1f;

    private bool onAir = true;

    // Update is called once per frame
    void Update()
    {

        float verticalInput = Input.GetAxis("Jump");
        if (verticalInput > jumpThreshold)
        {
            if (onAir)
            {
                pressTime += Time.deltaTime;
            }
            verticalMovement = verticalInput * jumpStrengthModifier;
        } else {
            pressTime = 0.0f;
            verticalMovement = 0.0f;
        }

        if (rigidbody.linearVelocityY < -downwardVelocityTrigger)
        {
            pressTime = Math.Clamp(pressTime -= Time.deltaTime * 2, 0.0f, pressTime);
        }

        rigidbody.gravityScale = 1 - Mathf.Clamp(pressTime - pressTimeMax, 0, pressTimeMax) * gravitySlowdownModifier;

        float horizontalInput = Input.GetAxis("Horizontal");
        horizontalMovement = horizontalInput * speed;
    }

    private void FixedUpdate()
    {
        rigidbody.linearVelocityX = horizontalMovement;
        if (footColider.IsTouchingLayers(Physics2D.GetLayerCollisionMask(10)))
        {
            if (onAir)
            {
                pressTime = 0;
            }
            onAir = false;
            rigidbody.AddForceY(verticalMovement);
        } else
        {
            onAir = true;
        }

    }
}
