using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    public Vector2 BGSize;

    private Transform player;

    private Transform back;

    public Vector3 offset;

    public Vector2 speed;

    private Vector3 startPos;
    private Vector3 loopingOffset;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Camera>().gameObject.transform;
        back = transform;
        startPos = new Vector3(player.position.x, player.position.y, 0);
        back.position = startPos + offset;
        loopingOffset = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        back.position = new Vector3(player.position.x, player.position.y, 0) - new Vector3(speed.x * (player.position.x - startPos.x), speed.y * (player.position.y - startPos.y), 0) + offset + loopingOffset;
        if (player.position.x - back.position.x > BGSize.x)
        {
            loopingOffset += new Vector3(BGSize.x, 0, 0);
        } else if (player.position.x - back.position.x < -BGSize.x)
        {
            loopingOffset -= new Vector3(BGSize.x, 0, 0);
        }
    }

    public void ResetPos()
    {
        player = FindObjectOfType<Camera>().gameObject.transform;
        back = transform;
        startPos = new Vector3(player.position.x, player.position.y, 0);
        back.position = startPos + offset;
        loopingOffset = new Vector3(0, 0, 0);
    }
}
