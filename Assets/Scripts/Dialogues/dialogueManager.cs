using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{

    private Queue<Sentence> sentences;

    public Text nameText;
    public Text dialogueText;
    public Text selectLeft;
    public Text selectRight;

    public Animator animator;
    public Animator animatorLeft;
    public Animator animatorRight;

    public bool onDialogue = false;

    public bool onSelect = false;

    private string thisProgressToBeAchieved;

    void Start()
    {
        sentences = new Queue<Sentence>();
    }

    public void StartDialogue(Sentence[] dialogueSentences, string progressToBeAchieved)
    {
        FindObjectOfType<gameManager>().SetMovement(false);

        animator.SetBool("isOpen", true);

        onDialogue = true;

        thisProgressToBeAchieved = progressToBeAchieved;
        sentences.Clear();

        foreach (Sentence sentence in dialogueSentences)
        {
            sentences.Enqueue(sentence);
        }

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Sentence sentence = sentences.Dequeue();

        if (sentence.name == "SELECT")
        {
            string[] content = sentence.sentence.Split('\n');

            onSelect = true;
            selectLeft.text = content[0];
            selectRight.text = content[1];
            animatorLeft.SetBool("open", true);
            animatorRight.SetBool("open", true);
            FindObjectOfType<interactManager>().PushNewLayer("InChatSelect");
        }
        else
        {
            nameText.text = sentence.name;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence.sentence));
        }
    }

    public void SelectLeft()
    {
        animatorLeft.SetBool("open", false);
        animatorRight.SetBool("open", false);
        onSelect = false;
        Debug.Log("Selected left.");
    }

    public void SelectRight()
    {
        animatorLeft.SetBool("open", false);
        animatorRight.SetBool("open", false);
        onSelect = false;
        Debug.Log("Selected right.");
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);

        onDialogue = false;

        FindObjectOfType<ProgressManager>().Achieve(thisProgressToBeAchieved);

        Invoke("RecoverMove", 0.5f);
    }

    void RecoverMove()
    {
        FindObjectOfType<gameManager>().SetMovement(true);
    }

    public void ClearDialogue()
    {
        animator.SetBool("isOpen", false);
        onDialogue = false;
        sentences.Clear();

        animatorLeft.SetBool("open", false);
        animatorRight.SetBool("open", false);
        onSelect = false;
    }

    private void Update()
    {
        if (onDialogue && !onSelect && Input.GetButtonDown("Jump") && !FindObjectOfType<gameManager>().pause)
        {
            NextSentence();
        }
    }
}
