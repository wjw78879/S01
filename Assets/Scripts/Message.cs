using UnityEngine;

public class Message : MonoBehaviour
{

    public GameObject message;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        message.SetActive(false);
    }
}
