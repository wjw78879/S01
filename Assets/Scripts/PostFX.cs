using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostFX : MonoBehaviour
{
    ColorGrading colorGrading;

    [Range(-100f, 100f)] public float sat;
    [Range(0f, 1f)] public float time;

    private void Start()
    {
        colorGrading = GetComponent<PostProcessVolume>().profile.GetSetting<ColorGrading>();
    }

    public void Pause()
    {
        StopAllCoroutines();
        StartCoroutine(tempDown(time));
    }

    public void Resume()
    {
        StopAllCoroutines();
        StartCoroutine(tempUp(time));
    }

    IEnumerator tempDown(float time)
    {
        float mul = Mathf.Pow(time, 0.1f);
        float target = sat + 0.001f;
        while (colorGrading.saturation.value > target)
        {
            colorGrading.saturation.value = (colorGrading.saturation.value + 100f) * mul - 100f;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        colorGrading.saturation.value =sat;
    }

    IEnumerator tempUp(float time)
    {
        float mul = Mathf.Pow(time, 0.1f);
        float target = -0.001f;
        while (colorGrading.saturation.value < target)
        {
            colorGrading.saturation.value = colorGrading.saturation.value * mul;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        colorGrading.saturation.value = 0f;
    }
}
