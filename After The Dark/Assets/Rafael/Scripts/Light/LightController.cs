using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float lightOnDuration = 120f; // 2 menit
    private float timer;
    private Light[] roomLights;
    public GameObject enemySpawner;

    private void Start()
    {
        roomLights = GameObject.FindGameObjectsWithTag("RoomLight")
                        .Select(obj => obj.GetComponent<Light>())
                        .ToArray();
        timer = 0;
        ToggleLights(true);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            ToggleLights(false);
            EnemySpawn spawner = enemySpawner.GetComponent<EnemySpawn>();
            spawner.EnemyDrop();
            timer = lightOnDuration;
        }
    }

    private void ToggleLights(bool state)
    {
        foreach (Light light in roomLights)
        {
            if (light != null)
            {
                light.enabled = state;
            }
            
        }
    }

    private IEnumerator DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null) // Cek apakah masih ada sebelum dihancurkan
            {
                Destroy(enemy);
                yield return new WaitForSeconds(0.01f); // Beri waktu untuk Unity memproses penghancuran
            }
        }
        Debug.Log("Enemy destroyed");
    }

    public void ResetLightTimer()
    {
        ToggleLights(true);
        timer = lightOnDuration;
        StartCoroutine(DestroyAllEnemies());
        //if (enemySpawner != null)
        //    enemySpawner.SetActive(false);
    }

    public void ForceTurnOffLights()
    {
        ToggleLights(false); // Matikan lampu tanpa menghancurkan musuh
        timer = 0;

        //if (enemySpawner != null)
        //{
        //    enemySpawner.SetActive(true); // Aktifkan spawner saat lampu mati
        //}
    }
}

