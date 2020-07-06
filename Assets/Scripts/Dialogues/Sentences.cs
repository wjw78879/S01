using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sentences
{
    public string progressRequired;
    public string achieveProgress;
    public Sentence[] sentences;
}

[System.Serializable]
public class Sentence
{
    [TextArea(1, 1)] public string name;
    [TextArea(1, 4)] public string sentence;
}