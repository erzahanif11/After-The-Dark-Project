using UnityEngine;

public class SkyboxTintChanger : MonoBehaviour
{
    public Material skyboxMaterial;      // Material Skybox
    public Gradient skyTintGradient;     // Gradient untuk perubahan warna langit

    [Range(18f, 24f)]
    public float currentTime = 18f;      // Waktu dalam jam (18:00 - 24:00)

    private void Update()
    {
        UpdateSkyTint();
    }

    private void UpdateSkyTint()
    {
        float timeNormalized = (currentTime - 18f) / (24f - 18f);

        if (skyboxMaterial != null)
        {
            // Mengubah warna Sky Tint berdasarkan waktu
            Color targetColor = skyTintGradient.Evaluate(timeNormalized);
            skyboxMaterial.SetColor("_SkyTint", targetColor);

            Debug.Log("Sky Tint Color: " + targetColor);
        }
    }
}
