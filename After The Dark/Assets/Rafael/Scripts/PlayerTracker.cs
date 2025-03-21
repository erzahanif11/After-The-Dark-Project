using UnityEngine;
using UnityEngine.UI;

public class PlayerTracker : MonoBehaviour
{
    public Transform player;               
    public RectTransform mapImage;         
    public RectTransform playerIcon;       
    public Vector2 worldMin;               
    public Vector2 worldMax;               

    void Update()
    {
        UpdatePlayerIcon();
    }

    void UpdatePlayerIcon()
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.z);

        Vector2 normalizedPos = new Vector2(
            Mathf.InverseLerp(worldMin.x, worldMax.x, playerPos.x),
            Mathf.InverseLerp(worldMin.y, worldMax.y, playerPos.y)
        );

        float iconX = (normalizedPos.x * mapImage.rect.width) - (mapImage.rect.width * 0.5f);
        float iconY = (normalizedPos.y * mapImage.rect.height) - (mapImage.rect.height * 0.5f);

        playerIcon.anchoredPosition = new Vector2(iconX, iconY);
    }
}