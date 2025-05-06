using UnityEngine;
using UnityEngine.UI;

public class HoldInteractable : MonoBehaviour
{
    public MainLOGIC tracker;
    public float holdDuration = 5f;  
    private float holdProgress = 0f;  

    public Slider progressBar; 
    private bool isPlayerInRange = false; 
    public Text InteractPrompt;
    private bool isComplete=false;
    private ChangeMaterialWithMeshRenderer changeMaterial;
    public LightController lightControllercs;
    public GameObject lightController;
    public AudioSource audioSource;
    public GameObject cross;


    void Start()
    {
        
        changeMaterial = GetComponent<ChangeMaterialWithMeshRenderer>();
        tracker = FindFirstObjectByType<MainLOGIC>();
        if (tracker == null)
        {
            Debug.LogError("MainLOGIC script not found in the scene!");
        }

        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(false); 
            progressBar.maxValue = holdDuration; 
            progressBar.value = holdProgress; 
        }
    }

    void Update()
    {
        if (isPlayerInRange && !isComplete && tracker.islightsOn ) 
        {
            if (Input.GetKey(KeyCode.E)) 
            {
                holdProgress += Time.deltaTime; 
                if (progressBar != null)
                    progressBar.value = holdProgress; 

                if (holdProgress >= holdDuration)
                {
                    isComplete = true;
                    Interact();
                    
                }
            }
        }

        if (progressBar != null)
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
                progressBar.transform.LookAt(mainCam.transform);
        }

        if (isComplete)
        {
            cross.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !isComplete & tracker.islightsOn)
        {
            isPlayerInRange = true;
            if (progressBar != null)
                progressBar.gameObject.SetActive(true); 
           
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (progressBar != null)
                progressBar.gameObject.SetActive(false); 
        }
    }

    void Interact()
    {
        audioSource.Play();
        changeMaterial.cleaned();
        if (progressBar != null)
            progressBar.gameObject.SetActive(false);


        holdProgress = 0f; 
        tracker.CompleteObjective();

        Debug.Log("Interaction Complete!");

    }
}
