using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    public float dayDurationInMinutes = 1f; // Durasi hari dalam menit nyata
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

        // Menghitung waktu berdasarkan durasi hari
        float timeIncrement = (6f / (dayDurationInMinutes * 60f)) * Time.deltaTime;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart Scene
    }
}
