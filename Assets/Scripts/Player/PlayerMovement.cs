using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement Section
    public float maxMoveSpeed;
    private float acceleration = 0.25f;
    private float deceleration = .5f;
    private float currentSpeed = 0;

    // Jumping Section
    public float jumpHeight;
    public float fallForce = 2.5f;
    public float lowFallForce = 2f;
    private bool isJumping = false;
    private bool isFalling = false;


    Rigidbody2D rb2d;

    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float getDirection = Input.GetAxis("Horizontal");

        // Acceleration - Deceleration
        if(getDirection != 0 && currentSpeed < maxMoveSpeed) currentSpeed += acceleration;
        else if(getDirection != 0 && currentSpeed >= maxMoveSpeed) currentSpeed = maxMoveSpeed;
        else if(getDirection == 0 && currentSpeed > 0) currentSpeed -= deceleration;
    
        // Movement
        transform.position = new Vector2(transform.position.x + getDirection * currentSpeed * Time.deltaTime, transform.position.y);
        if(getDirection < 0) GetComponent<SpriteRenderer>().flipX = true;
        else if (getDirection > 0) GetComponent<SpriteRenderer>().flipX = false;

        // Jump
        if(Input.GetButtonDown("Jump") && !isJumping) {
            isJumping = true;
            rb2d.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }

        if(rb2d.velocity.y < 0) {
            isFalling = true;
            rb2d.AddForce(new Vector2(0f, -fallForce), ForceMode2D.Force);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Floor") {
            isJumping = false;
            isFalling = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.collider.tag == "Floor") {
            isJumping = true;
            isFalling = false;
        }
    }
}
