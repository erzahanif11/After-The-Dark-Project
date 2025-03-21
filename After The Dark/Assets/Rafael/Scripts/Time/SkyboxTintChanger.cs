using UnityEngine;

public class SkyboxTintChanger : MonoBehaviour
{
    public Material skyboxMaterial;     
    public Gradient skyTintGradient;    

    [Range(18f, 24f)]
    public float currentTime = 18f;     

    private void Update()
    {
        UpdateSkyTint();
    }

    private void UpdateSkyTint()
    {
        float timeNormalized = (currentTime - 18f) / (24f - 18f);

        if (skyboxMaterial != null)
        {
            Color targetColor = skyTintGradient.Evaluate(timeNormalized);
            skyboxMaterial.SetColor("_SkyTint", targetColor);

            Debug.Log("Sky Tint Color: " + targetColor);
        }
    }
}
