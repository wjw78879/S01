using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
}
