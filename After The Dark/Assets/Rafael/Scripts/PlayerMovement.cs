using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector2 movement;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
            move.x -= moveSpeed * Time.deltaTime; 
        if (Input.GetKey(KeyCode.D))
            move.x += moveSpeed * Time.deltaTime; 
        if (Input.GetKey(KeyCode.W))
            move.z += moveSpeed * Time.deltaTime; 
        if (Input.GetKey(KeyCode.S))
            move.z -= moveSpeed * Time.deltaTime; 

        transform.position += move;

    }



}