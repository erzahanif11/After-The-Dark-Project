using UnityEngine;
using UnityEngine.UI;

public class LightOnInteract : MonoBehaviour
{
    public LightController lightController;
    public float holdDuration = 5f;  
    private float holdProgress = 0f;  

    public Slider progressBar; 
    private bool isPlayerInRange = false; 
    public Text InteractPrompt;
    public MainLOGIC Mainlogic;
    public ChangeMaterialWithMeshRenderer ChangeMaterialWithMeshRenderer;
    public AudioSource audioSource;

    void Start()
    {
        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(false); 
            progressBar.maxValue = holdDuration; 
            progressBar.value = holdProgress; 
        }
    }

    void Update()
    {
        if (isPlayerInRange && !Mainlogic.islightsOn) 
        {
            if (Input.GetKey(KeyCode.E))
            {
                holdProgress += Time.deltaTime; 
                if (progressBar != null)
                    progressBar.value = holdProgress; 

                if (holdProgress >= holdDuration)
                {
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Mainlogic.islightsOn)
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
        Debug.Log("Interaction Complete!");
        Mainlogic.islightsOn = true;
        if (lightController != null)
        {
            lightController.ResetLightTimer();
        }

        holdProgress = 0f;
        if (progressBar != null)
            progressBar.value = holdProgress;
        ChangeMaterialWithMeshRenderer.cleaned();
        progressBar.gameObject.SetActive(false);

    }
}
