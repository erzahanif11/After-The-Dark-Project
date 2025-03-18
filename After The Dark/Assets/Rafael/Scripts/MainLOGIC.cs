using NUnit.Framework;
using TMPro;  
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainLOGIC : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI objectivesText;  // Assign this in Inspector
    private int completedObjectives = 0;
    public int TargetObjectives = 6;
    public int NightTreshold = 3;
    public MainLOGIC mainlogic;
    public GameObject GameCompleteScreen;
    public GameObject clock;
    public GameObject LightController;
    public GameObject EnemySpawner;
    public LightController lightController;
    public GameObject flashlight;
    public bool islightsOn = true;
    



    //public void Start()
    //{
    //    if (flashlight != null)
    //    {
    //        flashlight.SetActive(false);
    //    }
    //}

    public void CompleteObjective()
    {
        completedObjectives++;
        objectivesText.text = $"Objectives Completed: {completedObjectives}/{TargetObjectives}";

        if (completedObjectives == TargetObjectives)
        {
            GameComplete();
        }
        if (completedObjectives == NightTreshold)
        {
            NightIsComing();  
        }
    }

    public void NightIsComing() {
        clock.SetActive(true);
        LightController.SetActive(true);
        EnemySpawner.SetActive(true);
            flashlight.SetActive(true);
        islightsOn=false;
        Debug.Log("flash nyala");
        

        LightController.GetComponent<LightController>().ForceTurnOffLights();
    }
    public void Playagain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameComplete() { 
        GameCompleteScreen.SetActive(true);
    }
  



}
