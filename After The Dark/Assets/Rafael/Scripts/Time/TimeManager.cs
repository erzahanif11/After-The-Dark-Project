using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    private float currentTime = 18f; // 18.00 berarti sore hari (jam 6 sore)
    private float endTime = 24f; // 24.00 berarti tengah malam

    [Header("UI Settings")]
    public Text timeText; // Tampilan jam dalam bentuk teks UI
    public GameObject gameOverPanel; // Panel Game Over

    [Header("Lighting Settings")]
    public Light directionalLight; // Lampu utama dalam scene
    public Gradient lightColor; // Warna lampu berdasarkan waktu
    public AnimationCurve lightIntensity; // Intensitas lampu berdasarkan waktu

    [Header("Skybox Settings")]
    public Material skyboxMaterial; // Skybox material
    public Gradient skyboxTintColor; // Perubahan warna skybox

    private bool isGameOver = false;
    public static TimeManager Instance;

    public void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        // Pastikan Panel Game Over tidak tampil di awal
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (isGameOver) return;

        // Menghitung waktu berdasarkan durasi hari 0.01 * deltatime = 1 menit 
        float timeIncrement =  Time.deltaTime/60f;
        currentTime += timeIncrement;

        // Update UI Jam
        UpdateTimeUI();

        // Update Pencahayaan
        UpdateLighting();

        // Cek jika waktu mencapai tengah malam
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

    // Mengupdate UI Jam
    private void UpdateTimeUI()
    {
        int hour = Mathf.FloorToInt(currentTime);
        int minute = Mathf.FloorToInt((currentTime - hour) * 60);
        timeText.text = string.Format("{0:00}:{1:00}", hour, minute);
    }

    // Mengupdate Pencahayaan dan Skybox
    private void UpdateLighting()
    {
        float timeNormalized = (currentTime - 18f) / (endTime - 18f); // Normalisasi waktu antara 18:00 hingga 24:00

        // Atur Warna & Intensitas Cahaya
        directionalLight.color = lightColor.Evaluate(timeNormalized);
        directionalLight.intensity = lightIntensity.Evaluate(timeNormalized);

        // Atur Warna Skybox jika ada
        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
            Color skyColor = skyboxTintColor.Evaluate(timeNormalized);
            skyboxMaterial.SetColor("_SkyTint", skyColor); // Ganti _Tint menjadi _SkyTint
            DynamicGI.UpdateEnvironment(); // Tambahkan ini untuk memastikan perubahan Global Illumination
        }

        // Atur Ambient Light sesuai perubahan waktu
        RenderSettings.ambientLight = Color.Lerp(Color.white, Color.black, timeNormalized);
    }

    // Fungsi Game Over
    private void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over! Sudah tengah malam.");
        isGameOver = true;

        // Sembunyikan Timer dan Tampilkan Game Over Panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (timeText != null)
        {
            timeText.gameObject.SetActive(false); // Sembunyikan Timer
        }
    }

    // Fungsi untuk tombol Restart
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart Scene
    }
}
