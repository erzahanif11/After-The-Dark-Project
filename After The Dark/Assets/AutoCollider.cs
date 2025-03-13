using UnityEngine;
using UnityEditor;

public class AutoCollider : MonoBehaviour
{
    [ContextMenu("Add Colliders")] // This adds a right-click option in Inspector
    void ApplyColliders()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<MeshFilter>() && !child.GetComponent<Collider>())
            {
                child.gameObject.AddComponent<MeshCollider>();
            }
        }
        Debug.Log("Colliders added successfully!");
    }
}
