using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Sentences[] sentences;

    public Sentence[] GetSentences()
    {
        return sentences[Random.Range(0, sentences.Length)].sentences;
    }
}
