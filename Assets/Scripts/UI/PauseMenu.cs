using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public Animator animator;

    public void Pause()
    {
        animator.SetBool("isOpen", true);
    }

    public void Resume()
    {
        animator.SetBool("isOpen", false);
    }
}
