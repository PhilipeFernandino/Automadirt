using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rb2DMovement : MonoBehaviour {   
    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D rb2D;
    
    Vector2 movement;
    float lastYPos;
    bool jump;

    void Update() {
        GetInput();
    }

    void FixedUpdate() {
        Move();
        lastYPos = rb2D.position.y;
    }

    void GetInput() {
        movement.x = Input.GetAxisRaw("Horizontal");
        // movement.y = Input.GetAxisRaw("Vertical");
        jump = Input.GetButton("Jump");
    }

    void Move() {
        Vector2 vel = rb2D.velocity;
        vel.x = movement.x * moveSpeed * Time.fixedDeltaTime;
        rb2D.velocity = vel;
        if (jump && Mathf.Approximately(rb2D.position.y, lastYPos)) {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            // rb2D.velocity = Vector2.up * jumpForce;
        }
    }
}
