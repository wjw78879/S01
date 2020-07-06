using UnityEngine;

public class gem : MonoBehaviour
{
    public GameObject fx;
    bool triggered;
    private void Start()
    {
        triggered = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && triggered == false)
        {
            triggered = true;
            FindObjectOfType<gameManager>().AddGem(1);
            FindObjectOfType<soundManager>().PlaySound("gemsfx");
            Instantiate(fx, transform.position, Quaternion.identity).transform.SetParent(FindObjectOfType<gameManager>().GetCurrentLevel().transform);
            Destroy(gameObject, 0.2f);
        }
    }
}
