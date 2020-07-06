using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHolder : MonoBehaviour
{
    public BGController[] bg;
    public Collider confiner3d;

    public void ResetBG()
    {
        foreach (BGController bgc in bg)
        {
            bgc.ResetPos();
        }
    }
}
