using UnityEngine;

public class FreeCameraMovement2D : MonoBehaviour {
    public float moveSpeed;
    public float mouseScrollWheelSensibility = 10f;

    
    private Vector2 movement;
    private Camera cam;
    private float zoom = 0f; 

    private void Awake() {
        cam = GetComponent<Camera>();    
    }

    private void Update() {
        GetInput();
        Move();
    }

    private void GetInput() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        zoom = Input.GetAxisRaw("Mouse ScrollWheel");
    }

    private void Move() {
        transform.position += (Vector3) movement * moveSpeed * Time.fixedDeltaTime * (cam.orthographicSize / 100f);
        cam.orthographicSize -= zoom * mouseScrollWheelSensibility;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 1f, 1000f);
    }
    
}
