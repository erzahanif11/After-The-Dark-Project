using UnityEngine;

public class Enter : MonoBehaviour
{
    public GameObject Intro;
    public GameObject panel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Intro.active && (Input.anyKeyDown))
        {
            panel.SetActive(false);
        }
    }
}
