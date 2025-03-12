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
            clock.SetActive(true);
        }
    }

    public void Playagain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameComplete() { 
        GameCompleteScreen.SetActive(true);
    }
  



}
