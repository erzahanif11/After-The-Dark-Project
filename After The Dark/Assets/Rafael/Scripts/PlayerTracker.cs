using UnityEngine;
using UnityEngine.UI;

public class PlayerTracker : MonoBehaviour
{
    public Transform player;               // Referensi ke Player di scene
    public RectTransform mapImage;         // Map UI di Canvas
    public RectTransform playerIcon;       // Ikon Player di Map
    public Vector2 worldMin;               // Koordinat Minimum di Dunia (kiri bawah)
    public Vector2 worldMax;               // Koordinat Maksimum di Dunia (kanan atas)

    void Update()
    {
        UpdatePlayerIcon();
    }

    void UpdatePlayerIcon()
    {
        // Ambil posisi player di dunia
        Vector2 playerPos = new Vector2(player.position.x, player.position.z);

        // Hitung posisi normalisasi (0 ke 1) berdasarkan dunia nyata
        Vector2 normalizedPos = new Vector2(
            Mathf.InverseLerp(worldMin.x, worldMax.x, playerPos.x),
            Mathf.InverseLerp(worldMin.y, worldMax.y, playerPos.y)
        );

        // Konversi ke posisi di Map UI
        float iconX = (normalizedPos.x * mapImage.rect.width) - (mapImage.rect.width * 0.5f);
        float iconY = (normalizedPos.y * mapImage.rect.height) - (mapImage.rect.height * 0.5f);

        // Set posisi ikon di map
        playerIcon.anchoredPosition = new Vector2(iconX, iconY);
    }
}