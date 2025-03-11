using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight; // The light component
    public Transform player; // Reference to the Player
    public LayerMask enemyLayer; // Layer for enemies
    public float flashlightRange = 5f; // Flashlight reach distance

    private bool facingRight = true;

    void Update()
    {
        // Determine player direction
        facingRight = player.localScale.x > 0; // Facing right if scale.x > 0

        // Adjust flashlight position and rotation
        transform.position = player.position + (facingRight ? Vector3.right : Vector3.left) * 0.5f;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);

        // Detect enemies using Raycast
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, flashlightRange, enemyLayer);

        foreach (var hit in hits)
        {
            EnemyAI enemy = hit.collider.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.Freeze();
            }
        }

        // Debugging: Draw flashlight beam in Scene view
        Debug.DrawRay(transform.position, transform.forward * flashlightRange, Color.yellow);
    }
}
