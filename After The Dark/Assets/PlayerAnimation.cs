using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Texture[] walkFrames; // Array of textures for animation
    public float frameRate = 0.1f; // Speed of animation

    private Renderer rend;
    private int currentFrame;
    private float timer;
    private Transform playerTransform;
    private Material playerMaterial;

    void Start()
    {
        rend = GetComponent<Renderer>();
        playerTransform = transform;
        playerMaterial = rend.material;
        currentFrame = 0;

        // Ensure Alpha Clipping is enabled in the material
        playerMaterial.SetFloat("_Cutoff", 0.5f);  // Adjust threshold if needed
        playerMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        playerMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        playerMaterial.SetInt("_ZWrite", 1);
        playerMaterial.EnableKeyword("_ALPHATEST_ON");
        playerMaterial.renderQueue = 2450; // Ensure correct rendering order
    }

    void Update()
    {
        // Check if any WASD key is pressed
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
                        Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
                        Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);    


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
                UpdateTexture(walkFrames[currentFrame]);
            }
        }
        else
        {
            UpdateTexture(walkFrames[0]); // Stop at first frame
        }
    }

    void UpdateTexture(Texture newTexture)
    {
        playerMaterial.SetTexture("_BaseMap", newTexture);
        playerMaterial.EnableKeyword("_ALPHATEST_ON");
    }
}
