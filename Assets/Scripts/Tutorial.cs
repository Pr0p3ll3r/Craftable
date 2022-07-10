using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public float delay = 0.1f;
    public string tut1;
    public string tut2;
    public string tut3;
    public string tut4;
    private string currentText = "";
    public TextMeshProUGUI text;
    private Animator animator;
    private int tut = 1;
    private Coroutine co;

    void Start()
    {
        Invoke("StartTyping", 1f);
        animator = GetComponent<Animator>();
    }

    void StartTyping()
    {
        co = StartCoroutine(ShowText(tut1));
    }

    IEnumerator ShowText(string fullText)
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            text.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    public void NextText()
    {
        AudioManager.Instance.Play("Click");
        StopCoroutine(co);
        if (tut == 1)
        {
            co = StartCoroutine(ShowText(tut2));
            tut++;
        }
        else if(tut == 2)
        {
            co = StartCoroutine(ShowText(tut3));
            tut++;
        }
        else if(tut == 3)
        {
            co = StartCoroutine(ShowText(tut4));
            tut++;
        }
        else
            animator.SetBool("isOpen", false);
    }
}