using UnityEngine;

public class mnimap : MonoBehaviour
{

    public GameObject gameObject;
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
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