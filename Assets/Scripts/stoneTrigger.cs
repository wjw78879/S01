using UnityEngine;

public class stoneTrigger : MonoBehaviour
{
    public Animator animator;

    private int triggerNumber = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            triggerNumber += 1;
            animator.SetBool("onTrigger", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (triggerNumber >= 1 && collision.gameObject.tag == "Player")
        {
            triggerNumber -= 1;
            if (triggerNumber == 0)
            {
                animator.SetBool("onTrigger", false);
            }
        }
    }
}
