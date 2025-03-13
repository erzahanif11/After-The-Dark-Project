using UnityEngine;

public class mnimap : MonoBehaviour
{

    public GameObject gameObject;
    void Start()
    {
        // Ensure the map starts hidden
        gameObject.SetActive(false);
    }

    void Update()
    {
        // Toggle map visibility based on M key being held
        if (Input.GetKey(KeyCode.M))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}