using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Texture[] walkFrames; 
    public float frameRate = 0.1f; 

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

        playerMaterial.SetFloat("_Cutoff", 0.5f);  
        playerMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        playerMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        playerMaterial.SetInt("_ZWrite", 1);
        playerMaterial.EnableKeyword("_ALPHATEST_ON");
        playerMaterial.renderQueue = 2450; 
    }

    void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
                        Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
                        Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerTransform.localScale = new Vector3(-1.64f, 1.64f, 1.64e-06f);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerTransform.localScale = new Vector3(1.64f, 1.64f, 1.64e-06f); 
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
            UpdateTexture(walkFrames[0]); 
        }
    }

    void UpdateTexture(Texture newTexture)
    {
        playerMaterial.SetTexture("_BaseMap", newTexture);
        playerMaterial.EnableKeyword("_ALPHATEST_ON");
    }
}
