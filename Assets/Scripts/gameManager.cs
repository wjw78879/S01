using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class gameManager : MonoBehaviour
{
    //Levels
    public GameObject menuScene;
    public Vector3 menuStartPos;
    public Level[] levelList;
    GameObject currentLevel;
    int currentLevelNo;

    //UI
    public GameObject HUD;
    public PauseMenu pauseMenu;
    public GameObject goal;
    public GameObject dieMenu;
    public GameObject title;
    public GameObject startText;
    public GameObject black;

    //Player
    public PlayerMovement move;
    public CharacterController2D controller;
    public Rigidbody2D rb2D;

    //Animators
    public Animator animPlayer;
    public Sprite idleImage;

    //Controllers
    public BGMPlayer BGMPlayer;
    public GameObject mainCamera;
    Cinemachine.CinemachineConfiner confiner;
    public SimpleCameraShakeInCinemachine shaker;
    public soundManager soundMan;
    public PostFX postFX;

    //statistics
    int gemnum;
    public Text gemText;
    public int PlayerHPMax;
    public int PlayerHP;
    public GameObject HPBar;
    int gemBuffer;

    //status
    public bool pause;
    public bool canEsc;
    bool canTakeDamage;
    bool prestarted;
    float startTime;

    private void Start()
    {
        confiner = mainCamera.GetComponent<Cinemachine.CinemachineConfiner>();

        SaveData saveData = SaveSystem.LoadPlayer();
        if (saveData != null && saveData.progress > 0)
        {
            currentLevel = Instantiate(levelList[saveData.levelNo].level);
            currentLevelNo = saveData.levelNo;

            mainCamera.SetActive(false);
            rb2D.position = new Vector3(saveData.playerX, saveData.playerY, saveData.playerZ);
            confiner.m_BoundingVolume = currentLevel.GetComponent<LevelHolder>().confiner3d;
            mainCamera.SetActive(true);

            HUD.SetActive(false);
            goal.SetActive(false);
            dieMenu.SetActive(false);

            pause = false;
            canEsc = true;
            canTakeDamage = true;
            prestarted = true;

            startText.SetActive(false);
            title.GetComponent<Animator>().SetBool("open", false);
            move.Starting();

            FindObjectOfType<ProgressManager>().LoadProgress(saveData.progress);

            return;
        }

        currentLevel = Instantiate(menuScene);
        currentLevelNo = -1;

        mainCamera.SetActive(false);
        rb2D.position = menuStartPos;
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        confiner.m_BoundingVolume = currentLevel.GetComponent<LevelHolder>().confiner3d;
        mainCamera.SetActive(true);

        HUD.SetActive(false);
        goal.SetActive(false);
        dieMenu.SetActive(false);

        pause = false;
        canEsc = false;
        canTakeDamage = false;
        prestarted = false;

        title.GetComponent<Animator>().SetBool("open", true);

        gemBuffer = 0;
        PlayerHP = PlayerHPMax;
        gemText.text = gemnum.ToString();

        //BGMPlayer.PlaySpecifiedBGM("B08", 0.1f, 0.3f);

        animPlayer.SetTrigger("fall");
        startTime = Time.realtimeSinceStartup;
        Time.timeScale = 0.5f;
    }
    private void Update()
    {
        /*if (canEsc && Input.GetButtonUp("Cancel"))
        {
            if (pause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }*/
        if (!prestarted && Input.GetButtonUp("Jump") && Time.realtimeSinceStartup - startTime > 2f)
        {
            //BGMPlayer.StopSpecifiedBGM("B08", 0.5f);
            Time.timeScale = 1.0f;
            FindObjectOfType<menuSceneManager>().Starting();
            mainCamera.SetActive(true);
            title.GetComponent<Animator>().SetBool("open", false);
            startText.SetActive(false);
            prestarted = true;
        }
    }

    /*void FixedUpdate()
    {
        if (pause)
        {
            controller.Move(0f, false, false);
        }
    }*/

    public void Starting()
    {
        shaker.Shake();
        soundMan.PlaySound("land");
        animPlayer.SetBool("started", true);
        animPlayer.enabled = false;
        animPlayer.enabled = true;
        Invoke("JumpAwake", 4);
    }

    private void JumpAwake()
    {
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        rb2D.AddForce(new Vector2(0, 1500f));
        FindObjectOfType<soundManager>().PlaySound("Jump");
        move.Starting();
        canEsc = true;
        canTakeDamage = true;

        FindObjectOfType<ProgressManager>().Achieve("STARTED");
    }

    public void Resume()
    {
        //Set UI
        pauseMenu.Resume();
        goal.SetActive(false);
        dieMenu.SetActive(false);

        //Set status
        pause = false;

        //Set movement&time
        StartCoroutine(LagResume());

        //Resume BGM
        BGMPlayer.Resume(0.6f);

        //FX
        postFX.Resume();
    }

    IEnumerator LagResume()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1f;
        if (!FindObjectOfType<dialogueManager>().onDialogue)
        {
            move.SetActive(true);
        }
    }

    public void Pause()
    {
        //Pause Time
        Time.timeScale = 0f;

        //Set UI
        pauseMenu.Pause();
        goal.SetActive(false);
        dieMenu.SetActive(false);

        //Set status
        pause = true;

        //Set movement
        move.SetActive(false);

        //Pause BGM
        BGMPlayer.Pause(0.6f);

        //FX
        postFX.Pause();
    }

    public void SetMovement(bool torf)
    {
        move.SetActive(torf);
    }

    public bool IsMoving()
    {
        return !move.Stopped();
    }

    public void AddGem(int num)
    {
        gemBuffer += num;
        gemText.text = (gemnum + gemBuffer).ToString();
    }

    public int GetGemNum()
    {
        return gemnum;
    }

    public void HPRecover(int num)
    {
        PlayerHP += num;
        if (PlayerHP > PlayerHPMax) PlayerHP = PlayerHPMax;
        HPBar.transform.localScale = new Vector3(((float)PlayerHP / PlayerHPMax), 1, 1);
    }

    public void TakeDamage(int num)
    {
        if (canTakeDamage)
        {
            FindObjectOfType<soundManager>().PlaySound("takeDamage");
            if (PlayerHP > num)
            {
                PlayerHP -= num;
                HPBar.transform.localScale = new Vector3(((float)PlayerHP / PlayerHPMax), 1, 1);
            }
            else
            {
                PlayerHP = 0;
                HPBar.transform.localScale = new Vector3(0, 1, 1);
                Die();
            }
        }
    }

    public GameObject GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SwitchLevel(int level, string direction, int spawnPoint)
    {
        move.Switching(direction);
        canEsc = false;
        black.GetComponent<Animator>().SetBool("black", true);
        StopAllCoroutines();
        StartCoroutine(LoadLevel(0.3f, level, spawnPoint));
    }

    IEnumerator LoadLevel(float delay, int level, int spawnPoint)
    {
        yield return new WaitForSecondsRealtime(1f);
        move.SetActive(true);
        buildLevel(level, spawnPoint);
        yield return new WaitForSecondsRealtime(delay);
        currentLevel.GetComponent<LevelHolder>().ResetBG();
        black.GetComponent<Animator>().SetBool("black", false);
    }

    void buildLevel(int level, int spawnPoint)
    {
        //Rebuild level
        Destroy(currentLevel);
        currentLevel = Instantiate(levelList[level].level);
        currentLevelNo = level;

        //Reset player & camera
        mainCamera.SetActive(false);
        controller.gameObject.transform.position = levelList[level].spawnPoints[spawnPoint];
        confiner.m_BoundingVolume = currentLevel.GetComponent<LevelHolder>().confiner3d;
        mainCamera.SetActive(true);

        //Reset UI & status
        if (pause) Resume();
        canEsc = true;
        goal.SetActive(false);
        dieMenu.SetActive(false);
        /*
        //Reset gem and HP
        gemBuffer = 0;
        gemText.text = gemnum.ToString();
        PlayerHP = PlayerHPMax;
        HPBar.transform.localScale = new Vector3(1, 1, 1);*/
    }

    public void levelChange(int level, int spawnPoint)
    {
        buildLevel(level, spawnPoint);
    }

    public void Restart()
    {
        pauseMenu.Resume();
        postFX.Resume();
        FindObjectOfType<dialogueManager>().ClearDialogue();
        pause = false;
        canEsc = false;

        black.GetComponent<Animator>().SetBool("black", true);
        
        StopAllCoroutines();
        StartCoroutine(ReloadGame(0.3f));
    }

    IEnumerator ReloadGame(float delay)
    {
        yield return new WaitForSecondsRealtime(1f);

        BGMPlayer.ClearAll();
        canTakeDamage = false;
        prestarted = false;        

        Destroy(currentLevel);
        currentLevel = Instantiate(menuScene);
        currentLevelNo = -1;

        FindObjectOfType<ProgressManager>().ClearProgress();
        FindObjectOfType<interactManager>().Clear();

        yield return new WaitForSecondsRealtime(delay);
        mainCamera.SetActive(false);
        rb2D.gameObject.SetActive(false);
        rb2D.gameObject.SetActive(true);
        rb2D.position = menuStartPos;
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        confiner.m_BoundingVolume = currentLevel.GetComponent<LevelHolder>().confiner3d;
        mainCamera.SetActive(true);

        title.GetComponent<Animator>().SetBool("open", true);
        startText.SetActive(true);
        //BGMPlayer.PlaySpecifiedBGM("B08", 0.1f, 0.1f);
        move.RestartGame();
        move.SetActive(true);
        animPlayer.SetBool("started", false);
        animPlayer.SetTrigger("fall");
        startTime = Time.realtimeSinceStartup;
        Time.timeScale = 0.5f;
        black.GetComponent<Animator>().SetBool("black", false);
    }

    public void Quit()
    {
        //Debug.Log("quit");
        Save();
        Application.Quit();
    }

    void Save()
    {
        float posX, posY, posZ;
        int level;
        if (currentLevelNo == -1)
        {
            posX = rb2D.gameObject.transform.position.x;
            posY = rb2D.gameObject.transform.position.y - FindObjectOfType<menuSceneManager>().GetWallY();
            posZ = rb2D.gameObject.transform.position.z;
            level = 0;
        }
        else
        {
            posX = rb2D.gameObject.transform.position.x;
            posY = rb2D.gameObject.transform.position.y;
            posZ = rb2D.gameObject.transform.position.z;
            level = currentLevelNo;
        }
        SaveSystem.SavePlayer(GetGemNum(), level, posX, posY, posZ, FindObjectOfType<ProgressManager>().GetProgressNo());
    }

    void Load()
    {
        SaveData data = SaveSystem.LoadPlayer();
        if (data == null) gemnum = 0;
        else { gemnum = data.gemnum; }
    }

    public void Clear()
    {
        //Set status
        pause = true;
        canEsc = false;

        //Set movement
        move.SetActive(false);

        //Set UI
        goal.SetActive(true);

        //Set BGM
        BGMPlayer.Pause(0.6f);
        FindObjectOfType<soundManager>().PlaySound("ClearBGM");

        //Confirm gem change
        gemnum += gemBuffer;
        gemBuffer = 0;
        gemText.text = gemnum.ToString();
    }

    void Die()
    {
        //Set status
        pause = true;
        canEsc = false;

        //Set movement
        move.SetActive(false);

        //Set UI
        dieMenu.SetActive(true);
    }
}
