using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    public Texture[] walkFrames; 
    public float frameRate = 0.1f; 

    private Renderer rend;
    private int currentFrame;
    private float timer;
    private Transform enemyTransform;
    private Material enemyMaterial;
    private NavMeshAgent agent;
    private EnemyAI enemyAI;

    void Start()
    {
        rend = GetComponent<Renderer>();
        enemyTransform = transform;
        enemyMaterial = rend.material;
        agent = GetComponent<NavMeshAgent>();
        enemyAI = GetComponent<EnemyAI>();
        currentFrame = 0;

        enemyMaterial.SetFloat("_Cutoff", 0.5f);
        enemyMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        enemyMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        enemyMaterial.SetInt("_ZWrite", 1);
        enemyMaterial.EnableKeyword("_ALPHATEST_ON");
        enemyMaterial.renderQueue = 2450;
    }

    void Update()
    {
        if (enemyAI != null && enemyAI.isFrozen)
        {
            UpdateTexture(walkFrames[0]); 
            return;
        }

        Vector3 velocity = agent.velocity;
        bool isMoving = velocity.sqrMagnitude > 0.0001f;

        Vector3 directionAwayFromZ = enemyTransform.position - new Vector3(enemyTransform.position.x, enemyTransform.position.y, 0);
        if (directionAwayFromZ != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionAwayFromZ);
            enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        if (velocity.x < 0)
        {
            enemyTransform.localScale = new Vector3(-1.64f, 1.64f, 1.64e-06f); 
        }
        else if (velocity.x > 0)
        {
            enemyTransform.localScale = new Vector3(1.64f, 1.64f, 1.64e-06f);
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
        enemyMaterial.SetTexture("_BaseMap", newTexture);
        enemyMaterial.EnableKeyword("_ALPHATEST_ON");
    }
}
