using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMConfiner : MonoBehaviour
{
    public string BGM;
    [Range(0f, 1f)] public float outTime;
    [Range(0f, 1f)] public float inTime;
    [Range(0f, 1f)] public float volume;
    public bool playFromTheBeginning;

    bool onTrigger = false;

    BGMPlayer player;

    private void Start()
    {
        player = FindObjectOfType<BGMPlayer>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onTrigger && collision.gameObject.tag == "Player")
        {
            player.StopSpecifiedBGM(BGM, outTime);
            onTrigger = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!onTrigger && collision.gameObject.tag == "Player")
        {
            player.PlaySpecifiedBGM(BGM, inTime, volume, playFromTheBeginning);
            onTrigger = true;
            //Debug.Log("Play " + BGM);
        }
    }
}
