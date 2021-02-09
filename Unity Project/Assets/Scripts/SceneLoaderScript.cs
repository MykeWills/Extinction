using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{

    private bool loadScene = false;
    public  static bool GameStart = false;
    [SerializeField]
    public static int scene = 2;
    [SerializeField]

    private Text loadingText;
    public GameObject Background;
    public GameObject Player;
    public GameObject MainGameCanvas;
    public GameObject MainMenuCanvas;
    public GameObject LoadSceneCanvas;
    public GameObject PauseMenu;
    GameObject StartingPosition;

    public AudioSource audioSrc;
    public AudioClip MainMenuMusic;
    public AudioClip Level1Music;
    public AudioClip Level2Music;
    public bool StartGameMusic;

    public int LevelCounter;

    public bool TurnOffCounterOne;
    public bool TurnOffCounterTwo;
    public bool TurnOffCounterThree;
    public bool TurnOffCounterFour;

    private void Start()
    {
        
    }

    void Update()
    {
        StartCoroutine(FindPosition());
        LevelCounter = AfterBossSceneLoading.LevelCounter;

     

        if (LevelCounter == 1 && !TurnOffCounterOne)
        {
            TurnOffCounterOne = true;
            Player.SetActive(false);
            scene = 3;
            loadScene = false;
        }
        if (LevelCounter == 2 && !TurnOffCounterTwo)
        {
            TurnOffCounterTwo = true;
            Player.SetActive(false);
            scene = 4;
            loadScene = false;
        }
        if (LevelCounter == 3 && !TurnOffCounterThree)
        {
            TurnOffCounterThree = true;
            Player.SetActive(false);
            scene = 3;
            loadScene = false;
        }
        if (LevelCounter == 4 && !TurnOffCounterFour)
        {
            TurnOffCounterFour = true;
            Player.SetActive(false);
            scene = 4;
            loadScene = false;
        }


        if (loadScene == false)
        {
            loadScene = true;
            loadingText.text = "Loading...";
            StartCoroutine(LoadNewScene());
        }
        if (loadScene == true)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
    }
    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
      
        while (!async.isDone)
        {
            yield return null;
        }
        if (async.isDone)
        {

            StartGame();

        }
       
    }
    IEnumerator FindPosition()
    {
        StartingPosition = GameObject.Find("Level1StartPosition");
        if (StartingPosition == null)
        {
            yield return null;
        }
        else
        {
            StartingPosition = GameObject.Find("Level1StartPosition");
            Player.transform.position = StartingPosition.transform.position;
        }
    }
    public void StartGame()
    {
        
        if (LevelCounter == 1)
        {
            audioSrc.clip = Level2Music;
            audioSrc.Play();
        }
        StartGameMusic = true;
        if (LevelCounter == 0 && StartGameMusic)
        {
            audioSrc.clip = Level1Music;
            audioSrc.Play();
            StartGameMusic = false;
        }
        MainGameCanvas.SetActive(true);
        Player.SetActive(true);
        PauseMenu.SetActive(true);
        LoadSceneCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        
    }
}