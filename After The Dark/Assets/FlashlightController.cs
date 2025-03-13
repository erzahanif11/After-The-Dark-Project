using UnityEngine;
using UnityEngine.Rendering;

public class FlashlightController : MonoBehaviour
{


    private Quaternion targetRotation;


    private void Start()
    {
        targetRotation= Quaternion.Euler(transform.eulerAngles); ;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            targetRotation = Quaternion.Euler(0, 270, 0); // Face left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetRotation = Quaternion.Euler(0, 90, 0); // Face right
        }
        else if (Input.GetKey(KeyCode.W))
        {
            targetRotation = Quaternion.Euler(0, 0, 0); // Face up
        }
        else if (Input.GetKey(KeyCode.S))
        {
            targetRotation = Quaternion.Euler(0, 180, 0); // Face down
        }

        // Instantly rotate to the target direction
        transform.rotation = targetRotation;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Ensure your enemies have the "Enemy" tag
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.Freeze(); // Call a freeze function in EnemyAI to stop them
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.Unfreeze(); // Restore enemy movement when out of light
            }
        }
    }
}
