using System.Linq;
using UnityEngine;

public class infoTrigger : MonoBehaviour
{
    public GameObject infoBoard;

    public TextMesh infoText;

    public Dialogue dialogue;

    public string characterName;

    public Animator animator;

    public infoText text;

    bool onTrigger = false;

    private void Start()
    {
        infoText.text = characterName + "\n按 X 谈话";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!onTrigger && collision.gameObject.tag == "Player")
        {
            onTrigger = true;
            animator.SetBool("show", true);
            //text.Show();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onTrigger = false;
        animator.SetBool("show", false);
        //text.Hide();
    }
    private void Update()
    {
        if (onTrigger == true && Input.GetButtonDown("Crouch") && FindObjectOfType<gameManager>().IsMoving())
        {
            animator.SetBool("show", false);

            int targetNo = 0;
            for (int i = 0; i < dialogue.sentences.Length; i++)
            {
                if (FindObjectOfType<ProgressManager>().HasAchieved(dialogue.sentences[i].progressRequired))
                {
                    targetNo = i;
                }
                else
                {
                    break;
                }
            }
            FindObjectOfType<dialogueManager>().StartDialogue(dialogue.sentences[targetNo].sentences, dialogue.sentences[targetNo].achieveProgress);
        }
    }
}
