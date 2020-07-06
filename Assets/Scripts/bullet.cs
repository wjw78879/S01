using UnityEngine;

public class bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bulletSpeedX = 10f;
    public float bulletSpeedY = 10f;
    soundManager soundManager;

    void Start()
    {
        rb.velocity =  transform.right * bulletSpeedX + new Vector3(0, bulletSpeedY,0);
        soundManager = FindObjectOfType<soundManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.isTrigger == false)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        soundManager.PlaySound("Hit");
        GetComponent<Animator>().SetBool("Hit", true);
        rb.gravityScale = 0;
        rb.velocity = new Vector3(0, 0, 0);
        Destroy(gameObject, 0.33f);
    }
}
