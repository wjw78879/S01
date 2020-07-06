using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private string[] progressList;

    private int currentProgressNo = 0;

    public int GetProgressNo()
    {
        return currentProgressNo;
    }

    public void LoadProgress(int progress)
    {
        currentProgressNo = progress;
    }

    public bool HasAchieved(string str)
    {
        for (int i = 0; i < progressList.Length; i++)
        {
            if (progressList[i] == str)
            {
                return currentProgressNo >= i;
            }
        }
        return false;
    }

    public void Achieve(string str)
    {
        for (int i = 0; i < progressList.Length; i++)
        {
            if (progressList[i] == str)
            {
                if(i - currentProgressNo > 1)
                {
                    Debug.Log((i - currentProgressNo - 1).ToString() + "progress skipped!");
                }
                currentProgressNo = i;
                return;
            }
        }
        Debug.Log("Progress name not found!");
    }

    public void ClearProgress()
    {
        currentProgressNo = 0;
    }
}
