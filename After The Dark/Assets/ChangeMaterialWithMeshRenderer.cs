using UnityEngine;

public class ChangeMaterialWithMeshRenderer : MonoBehaviour
{
    public Material newMaterial; 
    public Material trashedMaterial; 
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

    public void trashed()
    {
        if (meshRenderer != null && trashedMaterial != null)
        {
            meshRenderer.material = trashedMaterial;
        }
    }
}
