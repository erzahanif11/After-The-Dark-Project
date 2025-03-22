using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float lightOnDuration;
    private float timer;
    private Light[] roomLights;
    public GameObject enemySpawner;
    public bool isOff = true;
    public MainLOGIC mainlogic;
    public ChangeMaterialWithMeshRenderer ChangeMaterialWithMeshRenderer;
    public thedarktheme thedarkthemescript;
    public TimeManager time;
    private bool reduced = false;
    private void Start()
    {
        time = FindAnyObjectByType<TimeManager>();
        roomLights = GameObject.FindGameObjectsWithTag("RoomLight")
                        .Select(obj => obj.GetComponent<Light>())
                        .ToArray();
        timer = 0;
        ToggleLights(true);
        
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (time.currentTime >= 22 && !reduced)
        {
            lightOnDuration -= 30;
            reduced = true;
            Debug.Log("Durasi nyala lampu lebih sedikit");
        }
        if (timer <= 0)
        {
            ToggleLights(false);
            isOff= true;
            EnemySpawn spawner = enemySpawner.GetComponent<EnemySpawn>();
            StartCoroutine(spawner.EnemyDrop());
            timer = lightOnDuration;
            mainlogic.islightsOn = false;
            ChangeMaterialWithMeshRenderer.trashed();

        }
    }

    private void ToggleLights(bool state)
    {
        mainlogic.islightsOn = state;

        if (state) { 
            if(thedarkthemescript.thedarkthemeaudio.isPlaying)
                thedarkthemescript.thedarkthemeaudio.Stop();
        }
        else
        {
            thedarkthemescript.queTheMusic();
        }
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
        mainlogic.islightsOn = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
                yield return new WaitForSeconds(0.01f);
            }
        }
        Debug.Log("Enemy destroyed");
    }

    public void ResetLightTimer()
    {
        ToggleLights(true);
        timer = lightOnDuration;
        StartCoroutine(DestroyAllEnemies());
        mainlogic.islightsOn = true;
        ChangeMaterialWithMeshRenderer.cleaned();
    }

    public void ForceTurnOffLights()
    {
        ToggleLights(false); 
        timer = 0;
        isOff = true;
        mainlogic.islightsOn = false;
    }
}

