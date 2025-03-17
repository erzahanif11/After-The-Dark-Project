using UnityEngine;

public class ChangeMaterialWithMeshRenderer : MonoBehaviour
{
    public Material newMaterial; // Assign a new material in the Inspector
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void cleaned() 
    {
        if (meshRenderer != null && newMaterial != null)
        {
            meshRenderer.material = newMaterial;
        }
    }
}