using TMPro;  // Import TextMeshPro namespace
using UnityEngine;

public class ObjectiveTracker : MonoBehaviour
{
    public TextMeshProUGUI objectivesText;  // Assign this in Inspector
    private int completedObjectives = 0;

    public void CompleteObjective()
    {
        completedObjectives++;
        objectivesText.text = $"Objectives Completed: {completedObjectives}/3";
    }
}
