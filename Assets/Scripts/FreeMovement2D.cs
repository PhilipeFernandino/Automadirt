using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovement2D : MonoBehaviour {
    public float moveSpeed;
    Vector2 movement;

    void Update() {
        GetInput();
    }

    void FixedUpdate() {
        Move();
    }

    void GetInput() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void Move() {
        transform.position += (Vector3) movement * moveSpeed * Time.fixedDeltaTime;
    }
}
