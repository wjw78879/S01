using UnityEngine;
using System.Collections;

public class monster : MonoBehaviour
{
    public int health = 100;
    private int hp;
    public int damage;
    public GameObject deathFX;
    public Transform hpp;
    public float attackDelay;
    bool canAttack;
    bool isDied;

    public int cherryLoot_min;
    public int cherryLoot_max;
    public int gemLoot_min;
    public int gemLoot_max;
    public GameObject gemLoot;
    public GameObject cherryLoot;

    public int takeDamage;

    private void Start()
    {
        hp = health;
        canAttack = true;
        isDied = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullets")
        {
            TakeDamage(takeDamage);
            collision.gameObject.GetComponent<bullet>().Destroy();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canAttack)
        {
            if (collision.gameObject.tag == "Player")
            {
                int dir;
                StartCoroutine(waiter(attackDelay));
                FindObjectOfType<gameManager>().TakeDamage(damage);
                if (collision.gameObject.transform.position.x > transform.position.x) dir = 1;
                else dir = -1;
                FindObjectOfType<CharacterController2D>().BeAttacked(dir);
            }
        }
    }

    IEnumerator waiter(float time)
    {
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    public void TakeDamage(int damage)
    {
        if (!isDied)
        {
            hp -= damage;
            hpp.localScale = new Vector3(((float)hp / health * 3), 2, 3);
            if (hp <= 0)
            {
                isDied = true;
                Die();
            }
        }
    }

    public void Die()
    {
        Loot();
        Destroy(hpp.gameObject);
        FindObjectOfType<soundManager>().PlaySound("deathsfx");
        Instantiate(deathFX, transform.position, transform.rotation).transform.SetParent(FindObjectOfType<gameManager>().GetCurrentLevel().transform);
        Destroy(gameObject, 0.3f);
    }

    void Loot()
    {
        for(int i = 0; i < Random.Range(gemLoot_min, gemLoot_max); i++)
        {
            Instantiate(gemLoot, transform.position, Quaternion.identity).transform.SetParent(FindObjectOfType<gameManager>().GetCurrentLevel().transform);
        }
        for(int i = 0; i < Random.Range(cherryLoot_min, cherryLoot_max); i++)
        {
            Instantiate(cherryLoot, transform.position, Quaternion.identity).transform.SetParent(FindObjectOfType<gameManager>().GetCurrentLevel().transform);
        }
    }
}