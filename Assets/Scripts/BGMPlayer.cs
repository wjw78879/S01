using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public string[] BGMNames;
    public AudioClip[] BGMs;
    Dictionary<string, AudioSource> audioSources;
    Dictionary<string, Coroutine> coroutines;
    Dictionary<string, bool> isPlaying;
    Dictionary<string, float> playVolume;
    public GameObject playerPrefab;

    public float defaultVolume;

    //string currentBGM = "";

    private void Start()
    {
        audioSources = new Dictionary<string, AudioSource>();
        coroutines = new Dictionary<string, Coroutine>();
        isPlaying = new Dictionary<string, bool>();
        playVolume = new Dictionary<string, float>();
    }


    /*public void PlayBGM(string bgm, float outTime, float inTime)
    {
        if (currentBGM != "")
        {
            if (coroutines.ContainsKey(currentBGM) && coroutines[currentBGM] != null)
            {
                StopCoroutine(coroutines[currentBGM]);
            }
            coroutines[currentBGM] = StartCoroutine(FadeOut(currentBGM, outTime));
        }

        if (bgm == "")
        {
            currentBGM = "";
            return;
        }

        if (coroutines.ContainsKey(bgm) && coroutines[bgm] != null)
        {
            StopCoroutine(coroutines[bgm]);
        }
        coroutines[bgm] = StartCoroutine(FadeIn(bgm, inTime));

        currentBGM = bgm;
    }*/

    public void Pause(float outTime)
    {
        foreach (string bgm in audioSources.Keys)
        {
            if (coroutines.ContainsKey(bgm) && coroutines[bgm] != null)
            {
                StopCoroutine(coroutines[bgm]);
            }
            coroutines[bgm] = StartCoroutine(FadeOut(bgm, outTime, pause:true));
        }
    }

    public void Resume(float inTime)
    {
        foreach (string bgm in audioSources.Keys)
        {
            if (isPlaying[bgm])
            {
                if (coroutines.ContainsKey(bgm) && coroutines[bgm] != null)
                {
                    StopCoroutine(coroutines[bgm]);
                }
                coroutines[bgm] = StartCoroutine(FadeIn(bgm, inTime, playVolume[bgm]));
            }
        }
    }

    public void PlaySpecifiedBGM(string bgmName, float inTime, float volume, bool restart=false)
    {
        if (restart && audioSources.ContainsKey(bgmName) && audioSources[bgmName] != null)
        {
            if (coroutines.ContainsKey(bgmName) && coroutines[bgmName] != null)
            {
                StopCoroutine(coroutines[bgmName]);
            }
            Destroy(audioSources[bgmName].gameObject);
        }
        
        if (coroutines.ContainsKey(bgmName) && coroutines[bgmName] != null)
        {
            StopCoroutine(coroutines[bgmName]);
        }
        coroutines[bgmName] = StartCoroutine(FadeIn(bgmName, inTime, volume));
        playVolume[bgmName] = volume;
        isPlaying[bgmName] = true;
    }

    public void StopSpecifiedBGM(string bgmName, float outTime)
    {
        if (audioSources.ContainsKey(bgmName))
        {
            if (coroutines.ContainsKey(bgmName) && coroutines[bgmName] != null)
            {
                StopCoroutine(coroutines[bgmName]);
            }
            coroutines[bgmName] = StartCoroutine(FadeOut(bgmName, outTime));
            isPlaying[bgmName] = false;
        }
    }

    public void ClearAll()
    {
        StopAllCoroutines();
        foreach (string name in audioSources.Keys)
        {
            Destroy(audioSources[name].gameObject);
        }
        audioSources.Clear();
    }

    /*public string CurrentBGM()
    {
        return currentBGM;
    }*/

    IEnumerator FadeOut(string bgm, float outTime, bool pause=false)
    {
        float mul = Mathf.Pow(outTime, 0.1f);
        float target = 0.001f;
        while (audioSources[bgm].volume > target)
        {
            audioSources[bgm].volume *= mul;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        audioSources[bgm].volume = 0f;
        if (pause)
        {
            audioSources[bgm].Pause();
        }
    }

    IEnumerator FadeIn(string bgm, float inTime, float volume)
    {
        if (!audioSources.ContainsKey(bgm))
        {
            audioSources[bgm] = Instantiate(playerPrefab).GetComponent<AudioSource>();
            audioSources[bgm].clip = BGMs[Index(BGMNames, bgm)];
            audioSources[bgm].Play();
            audioSources[bgm].volume = 0f;
        }
        else
        {
            audioSources[bgm].UnPause();
        }
        float mul = Mathf.Pow(inTime, 0.1f);
        float target = volume - 0.001f;
        while (audioSources[bgm].volume < target)
        {
            audioSources[bgm].volume = volume - (volume - audioSources[bgm].volume) * mul;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        audioSources[bgm].volume = volume;
    }

    private int Index(string[] arr, string str)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == str)
            {
                return i;
            }
        }
        return -1;
    }
}
