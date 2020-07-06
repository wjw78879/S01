using UnityEngine;

public class LootCherry : MonoBehaviour
{
    public GameObject fx;
    bool triggered;
    gameManager gameManager;
    public int recoverNum;
    public float jumpForce;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f) * jumpForce, jumpForce));
        GetComponent<Rigidbody2D>().freezeRotation = true;
        triggered = false;
        gameManager = FindObjectOfType<gameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && triggered == false && gameManager.PlayerHP < gameManager.PlayerHPMax)
        {
            triggered = true;
            gameManager.HPRecover(recoverNum);
            FindObjectOfType<soundManager>().PlaySound("healthRecover");
            Instantiate(fx, transform.position, Quaternion.identity).transform.SetParent(gameManager.GetCurrentLevel().transform);
            Destroy(gameObject, 0.2f);
        }
    }
}
