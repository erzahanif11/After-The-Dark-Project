using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float lightOnDuration = 120f; // 2 menit
    private float timer;
    private Light[] roomLights;

    private void Start()
    {
        roomLights = GameObject.FindGameObjectsWithTag("RoomLight")
                        .Select(obj => obj.GetComponent<Light>())
                        .ToArray();
        timer = lightOnDuration;
        ToggleLights(true);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            ToggleLights(false);
            //StartCoroutine(TurnOnLightsAfterDelay(1f));
            timer = lightOnDuration;
        }
    }

    private void ToggleLights(bool state)
    {
        foreach (Light light in roomLights)
        {
            if (light != null)
                light.enabled = state;
        }
    }

    public void ResetLightTimer()
    {
        ToggleLights(true);
        timer = lightOnDuration;
    }
}
