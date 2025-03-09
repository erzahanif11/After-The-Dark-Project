using UnityEngine;

public class MovementV2 : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal"); // Left / Right
        movement.z = Input.GetAxisRaw("Vertical");   // Forward (North) / Backward (South)
    }

    void FixedUpdate()
    {
        // Move the player
        rb.linearVelocity = new Vector3(movement.x * moveSpeed, rb.linearVelocity.y, movement.z * moveSpeed);
    }
}
