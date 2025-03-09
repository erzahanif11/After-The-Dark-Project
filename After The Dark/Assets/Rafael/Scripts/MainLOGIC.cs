using TMPro;  
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainLOGIC : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI objectivesText;  // Assign this in Inspector
    private int completedObjectives = 0;
    public int TargetObjectives = 3;
    public MainLOGIC mainlogic;
    public GameObject GameCompleteScreen;




    public void CompleteObjective()
    {
        completedObjectives++;
        objectivesText.text = $"Objectives Completed: {completedObjectives}/{TargetObjectives}";

        if (completedObjectives == TargetObjectives) {
            GameComplete();

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
