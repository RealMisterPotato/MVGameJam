using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool onGround = true;
    private float skateForce = 20.0f;
    private Vector2 velocity = new Vector2(0, 0);

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // send data to animator
        if (animator != null) animator.SetFloat("GroundSpeed", Math.Abs(rigidBody.velocity.x));
        // flip sprite if needed
        spriteRenderer.flipX = (velocity.x < 0);
    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        // handle keys
        if (Input.GetKey(KeyCode.A)) LeftKeyPressed();
        if (Input.GetKey(KeyCode.D)) RightKeyPressed();
        if (Input.GetKeyDown(KeyCode.W)) JumpKeyPressed();

        // movement stuff
        float velocityXZeroOffset = 0.001f;
        if (-velocityXZeroOffset <= rigidBody.velocity.x && rigidBody.velocity.x <= velocityXZeroOffset) rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
    }

    // Skate with a certain force (can be negative)
    private void Skate(float force)
    {
        if (onGround)
        {
            rigidBody.velocity += new Vector2(force * Time.fixedDeltaTime, 0);
        }
    }
    // Handle keys

    private void LeftKeyPressed()
    {
        if (velocity.x <= 0)
            Skate(-skateForce);
    }
    private void RightKeyPressed()
    {
        if (0 <= velocity.x)
            Skate(skateForce);
    }
    private void JumpKeyPressed()
    {
        if (onGround)
            rigidBody.AddForce(new Vector2(0, 50.0f));
    }
}
