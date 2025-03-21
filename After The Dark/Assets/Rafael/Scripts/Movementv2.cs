using UnityEngine;

public class MovementV2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.z = Input.GetAxisRaw("Vertical");  
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(movement.x * moveSpeed, rb.linearVelocity.y, movement.z * moveSpeed);
    }
}
