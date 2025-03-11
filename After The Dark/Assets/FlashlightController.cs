using UnityEngine;

public class FlashlightController : MonoBehaviour
{
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
