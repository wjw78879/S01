using UnityEngine;

public class landTrigger : MonoBehaviour
{
    public Transform transWall;
    public Transform transMiddle;
    public Transform transDirtPos;

    Transform transPlayer;

    private bool dirted = false;

    private void Start()
    {
        transPlayer = FindObjectOfType<CharacterController2D>().gameObject.transform;
    }

    private void FixedUpdate()
    {
        if (!dirted && transPlayer.position.y <= transDirtPos.position.y)
        {
            FindObjectOfType<menuSceneManager>().Dirt(transDirtPos);
            dirted = true;
        }
        if (transPlayer.position.y <= transform.position.y)
        {
            FindObjectOfType<menuSceneManager>().Landed(transWall.position, transMiddle.position);
            Destroy(gameObject);
        }
    }
}
