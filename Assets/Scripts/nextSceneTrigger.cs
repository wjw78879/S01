using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextSceneTrigger : MonoBehaviour
{
    bool onTrigger = false;
    public string direction;
    public int level;
    public int spawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!onTrigger && collision.gameObject.tag == "Player")
        {
            onTrigger = true;
            FindObjectOfType<gameManager>().SwitchLevel(level, direction, spawnPoint);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(onTrigger && collision.gameObject.tag == "Player")
        {
            onTrigger = false;
        }
    }
}
