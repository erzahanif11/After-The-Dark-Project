using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Texture[] walkFramesHorizontal;
    public Texture[] walkFramesVertical;
    public float frameRate = 0.1f;
    public float audioRate = 0.1f;
    public AudioClip[] audioClips;
    public float audioVolume = 1.0f; 
    private Renderer rend;
    private int currentFrameH;
    private int currentFrameV;
    private float timer;
    private float timer2;
    private Transform playerTransform;
    private Material playerMaterial;
    private AudioSource audioSource;
    private int toggle = 0;
    private bool lastframeisH;
    public float ketebalan=1.6f;
    private Rigidbody rb;
    void Start()
    {
        rend = GetComponent<Renderer>();
        playerTransform = transform;
        playerMaterial = rend.material;
        currentFrameH = 0;
        rb= GetComponent<Rigidbody>();

        playerMaterial.SetFloat("_Cutoff", 0.5f);
        playerMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        playerMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        playerMaterial.SetInt("_ZWrite", 1);
        playerMaterial.EnableKeyword("_ALPHATEST_ON");
        playerMaterial.renderQueue = 2450;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = audioVolume; 
    }

    void Update()
    {
        bool isMoving = rb.linearVelocity.magnitude!=0 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
                        Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
                        Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));

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
            timer2 += Time.deltaTime;
            if (timer >= frameRate)
            {
                timer = 0f;

                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow))
                {
                    playerTransform.localScale = new Vector3(ketebalan, 1.64f, 1.64e-06f);
                    lastframeisH = false;
                    currentFrameH = 0;
                    currentFrameV = (currentFrameV + 1) % walkFramesVertical.Length;
                    UpdateTexture(walkFramesVertical[currentFrameV]);
                }
                else
                {
                    lastframeisH = true;
                    currentFrameV = 0;
                    currentFrameH = (currentFrameH + 1) % walkFramesHorizontal.Length;
                    UpdateTexture(walkFramesHorizontal[currentFrameH]);
                }
            }
            if (timer2 >= audioRate)
            {
                timer2 = 0f;
                audioSource.PlayOneShot(audioClips[toggle]);
                toggle = 1 - toggle;
            }
        }
        else
        {
            if (lastframeisH)
                UpdateTexture(walkFramesHorizontal[0]);
            else
                UpdateTexture(walkFramesVertical[0]);
        }
    }

    void UpdateTexture(Texture newTexture)
    {
        playerMaterial.SetTexture("_BaseMap", newTexture);
        playerMaterial.EnableKeyword("_ALPHATEST_ON");
    }
}
