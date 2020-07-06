using UnityEngine;
using System.Collections;

public class infoText : MonoBehaviour
{
    public TextMesh text;

    public float increment;

    void Start()
    {
        GetComponent<MeshRenderer>().sortingLayerName = "ForeGround";
        GetComponent<MeshRenderer>().sortingOrder = 6;
    }

    public void Show()
    {
        StopAllCoroutines();
        StartCoroutine(AlphaUp(increment));
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(AlphaDown(increment));
    }

    IEnumerator AlphaUp(float increment)
    {
        while (text.color.a < 1f - increment)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + increment);
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }

    IEnumerator AlphaDown(float increment)
    {
        while (text.color.a > increment)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - increment);
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
    }
}
