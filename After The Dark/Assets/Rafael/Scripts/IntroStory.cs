using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class IntroStory : MonoBehaviour
{
    public GameObject Intro;
    public TMP_Text storyText;
    public string[] lines;
    public float textSpeed = 0.05f;

    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool lineFinished = false;
    private Coroutine typingCoroutine;

    private void Start()
    {
        storyText.text = "";
        typingCoroutine = StartCoroutine(TypeLine(lines[currentLineIndex]));
    }

    private void Update()
    {
        if (!isTyping && Input.anyKeyDown)
        {
            NextLine();
        }
    }

    void NextLine()
    {
        storyText.text = "";
        lineFinished = false;
        currentLineIndex++;

        if (currentLineIndex < lines.Length)
        {
            StartCoroutine(TypeLine(lines[currentLineIndex]));
        }
        else
        {
            Intro.SetActive(false);
        }
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        storyText.text = "";

        foreach (char c in line)
        {
            storyText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        lineFinished = true;
    }
}
