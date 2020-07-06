using UnityEngine;

public class deathFX : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }
}
