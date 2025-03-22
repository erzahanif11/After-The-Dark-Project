using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    public float currentTime = 18f; 
    private float endTime = 24f; 

    [Header("UI Settings")]
    public Text timeText; 
    public GameObject gameOverPanel; 

    [Header("Lighting Settings")]
    public Light directionalLight; 
    public Gradient lightColor; 
    public AnimationCurve lightIntensity; 

    [Header("Skybox Settings")]
    public Material skyboxMaterial; 
    public Gradient skyboxTintColor; 

    private bool isGameOver = false;
    public static TimeManager Instance;

    public void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (isGameOver) return;

        float timeIncrement =  Time.deltaTime/60f;
        currentTime += timeIncrement;

        UpdateTimeUI();

        UpdateLighting();

        if (currentTime >= endTime)
        {
            GameOver();
        }
    }

    public void AddTime()
    {
        currentTime += 0.25f;   
        Debug.Log("Waktu bertambah 15 menit");
    }

    private void UpdateTimeUI()
    {
        int hour = Mathf.FloorToInt(currentTime);
        int minute = Mathf.FloorToInt((currentTime - hour) * 60);
        timeText.text = string.Format("{0:00}:{1:00}", hour, minute);
    }

    private void UpdateLighting()
    {
        float timeNormalized = (currentTime - 18f) / (endTime - 18f); 

        directionalLight.color = lightColor.Evaluate(timeNormalized);
        directionalLight.intensity = lightIntensity.Evaluate(timeNormalized);

        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
            Color skyColor = skyboxTintColor.Evaluate(timeNormalized);
            skyboxMaterial.SetColor("_SkyTint", skyColor); 
            DynamicGI.UpdateEnvironment(); 
        }

        RenderSettings.ambientLight = Color.Lerp(Color.white, Color.black, timeNormalized);
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over! Sudah tengah malam.");
        isGameOver = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (timeText != null)
        {
            timeText.gameObject.SetActive(false); 
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
