using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Texture[] walkFrames;  // Drag textures into this array in Inspector
    public float frameRate = 0.1f; // Adjust animation speed

    private Renderer rend;
    private int currentFrame;
    private float timer;

    void Start()
    {
        rend = GetComponent<Renderer>();
        currentFrame = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % walkFrames.Length;
            rend.material.SetTexture("_BaseMap", walkFrames[currentFrame]); // URP uses _BaseMap
        }
    }
}
