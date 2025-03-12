using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Texture[] walkFrames; // Array of textures for animation
    public float frameRate = 0.1f; // Speed of animation

    private Renderer rend;
    private int currentFrame;
    private float timer;
    private Transform playerTransform;

    void Start()
    {
        rend = GetComponent<Renderer>();
        playerTransform = transform;
        currentFrame = 0;
    }

    void Update()
    {
        // Check if any WASD key is pressed
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (Input.GetKey(KeyCode.A))
        {
            playerTransform.localScale = new Vector3(-1.64f, 1.64f, 1.64e-06f); // Flip left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerTransform.localScale = new Vector3(1.64f, 1.64f, 1.64e-06f); // Face right (default)
        }

        if (isMoving)
        {
            timer += Time.deltaTime;
            if (timer >= frameRate)
            {
                timer = 0f;
                currentFrame = (currentFrame + 1) % walkFrames.Length;
                rend.material.SetTexture("_BaseMap", walkFrames[currentFrame]);
            }
        }
        else
        {
            rend.material.SetTexture("_BaseMap", walkFrames[0]); // Stop at first frame
        }
    }
}
