using UnityEngine;

public class menuSceneManager : MonoBehaviour
{
    public Animator animWall;
    public Animator animMiddle;

    public GameObject dirt;

    public Collider2D colTerrain;

    private void Start()
    {
        colTerrain.enabled = false;
    }

    public void Starting()
    {
        animWall.SetTrigger("stop");
        animMiddle.SetTrigger("stop");
    }

    public void Landed(Vector3 posWall, Vector3 posMiddle)
    {
        animWall.enabled = false;
        animMiddle.enabled = false;
        animWall.gameObject.transform.position = posWall;
        animMiddle.gameObject.transform.position = posMiddle;
        FindObjectOfType<gameManager>().Starting();
        Invoke("TerrainAwake", 4.5f);
    }

    private void TerrainAwake()
    {
        colTerrain.enabled = true;
    }

    public void Dirt(Transform transPos)
    {
        Instantiate(dirt, transPos.position, Quaternion.identity).transform.SetParent(animWall.gameObject.transform);
    }

    public float GetWallY()
    {
        return animWall.gameObject.transform.position.y;
    }
}
