using UnityEngine;
using UnityEngine.UI;

public class HoldInteractable : MonoBehaviour
{
    public float holdDuration = 30f;  // Time required to complete interaction
    private float holdProgress = 0f;  // Tracks progress when holding the button

    public Slider progressBar; // UI slider reference
    private bool isPlayerInRange = false; // To check if player is inside the trigger

    void Start()
    {
        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(false); // Hide progress bar initially
            progressBar.maxValue = holdDuration; // Set max value
            progressBar.value = holdProgress; // Set starting value
        }
    }

    void Update()
    {
        if (isPlayerInRange) // Only run logic if the player is inside the trigger
        {
            if (Input.GetKey(KeyCode.E)) // Holding the key
            {
                holdProgress += Time.deltaTime; // Increment progress
                if (progressBar != null)
                    progressBar.value = holdProgress; // Update UI bar

                Debug.Log("Holding progress: " + holdProgress.ToString("F2") + "s");

                if (holdProgress >= holdDuration)
                {
                    Interact();
                }
            }
        }

        // Ensure the progress bar always faces the player
        if (progressBar != null)
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
                progressBar.transform.LookAt(mainCam.transform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (progressBar != null)
                progressBar.gameObject.SetActive(true); // Show progress bar when in range
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (progressBar != null)
                progressBar.gameObject.SetActive(false); // Hide bar when leaving
        }
    }

    void Interact()
    {
        Debug.Log("Interaction Complete!");
        holdProgress = 0f; // Reset progress after completion
        if (progressBar != null)
            progressBar.value = holdProgress; // Reset UI bar


        // logika selnju
    }
}
