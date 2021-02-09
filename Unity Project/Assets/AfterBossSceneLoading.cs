using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AfterBossSceneLoading : MonoBehaviour {

    public int BossCounter;
    public static int LevelCounter;
    public bool AddLevelCounter;
    public float TimerCountDown;
    bool FoundPosition;

    public static bool BossDead;

    public GameObject LoadingScreenCanvas;
    public GameObject MainGameCanvas;
    public GameObject Player;
    public GameObject PauseMenu;

    GameObject Level1StartPosition;
    GameObject Level2StartPosition;
    GameObject Level3StartPosition;
    GameObject Level4StartPosition;
    GameObject Level5StartPosition;

    Text UIText;

    public static Vector3 Position;

    public bool ShutOffBossCounterOne;
    public bool ShutOffBossCounterTwo;
    public bool ShutOffBossCounterThree;
    public bool ShutOffBossCounterFour;
   
    float CountDownTimer;
    
    public Transform camTransform;
    Vector3 originalPos;

    public float shakeAmount = 0.7f;

    // Use this for initialization
    void Start ()
    {
        CountDownTimer = TimerCountDown;
        AddLevelCounter = false;
        ShutOffBossCounterOne = false;
        ShutOffBossCounterTwo = false;
        ShutOffBossCounterThree = false;
        ShutOffBossCounterFour = false;
        Position = gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        BossCounter = OmegaController.BossCounter + JonNewBossController.BossCounter;

        //===================================BossCounters==================================//
        if (BossCounter == 1 && !ShutOffBossCounterOne)
        {
            StartCountDown();
            if (AddLevelCounter == false)
            {
                FoundPosition = false;
                LevelCounter++;
                AddLevelCounter = true;
            }
        }
        if (BossCounter == 2 && !ShutOffBossCounterTwo)
        {
            StartCountDown();
            if (AddLevelCounter == false)
            {
                FoundPosition = false;
                LevelCounter++;
                AddLevelCounter = true;
            }
        }
        if (BossCounter == 3 && !ShutOffBossCounterThree)
        {
            StartCountDown();
            if (AddLevelCounter == false)
            {
                FoundPosition = false;
                LevelCounter++;
                AddLevelCounter = true;
            }
        }
        if (BossCounter == 4 && !ShutOffBossCounterFour)
        {
            StartCountDown();
            if (AddLevelCounter == false)
            {
                FoundPosition = false;
                LevelCounter++;
                AddLevelCounter = true;
            }
        }
        //===================================PositionCoroutines==================================//
        if (BossCounter == 0 && !FoundPosition)
        {
            StartCoroutine(WaitForLevel1Position());
        }
        if (BossCounter == 1 && !FoundPosition)
        {
            StartCoroutine(WaitForLevel2Position());
        }
        if (BossCounter == 2 && !FoundPosition)
        {
            StartCoroutine(WaitForLevel3Position());
        }
        if (BossCounter == 3 && !FoundPosition)
        {
            StartCoroutine(WaitForLevel4Position());
        }
        if (BossCounter == 4 && !FoundPosition)
        {
            StartCoroutine(WaitForLevel5Position());
        }
    }

    //======================================Methods==============================//

    void StartCountDown()
    {
        StartCoroutine(WaitForText());
        
        if (CountDownTimer > 0)
        {
            CountDownTimer -= Time.deltaTime;
            BossDead = true;
        }
        UIText.text = "Time: " + (int)CountDownTimer;
        camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
        if (CountDownTimer <= 30)
        {
            UIText.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        }
        if (CountDownTimer >= 31)
        {
            UIText.color = new Color(1f, 1f, 1f, 1f);
        }
        if (CountDownTimer <= 0)
        {
            BossDead = false;
            if(BossCounter == 1)
            {
                ShutOffBossCounterOne = true;
            
            }
            if (BossCounter == 2)
            {
                ShutOffBossCounterTwo = true;
               
            }
            if (BossCounter == 3)
            {
                ShutOffBossCounterThree = true;
                
            }
            if (BossCounter == 4)
            {
                ShutOffBossCounterFour = true;
                
            }
            CountDownTimer += TimerCountDown;
            LoadLevel();
            
        }
    }
    public void LoadLevel()
    {
        UIText.text = "";
        UIText.color = new Color(1f, 1f, 1f, 1f);
        SceneManager.LoadScene(1);
        MainGameCanvas.SetActive(false);
        Player.SetActive(false);
        PauseMenu.SetActive(false);
        LoadingScreenCanvas.SetActive(true);
        
    }
    //======================================TextEnumerators==============================//

    IEnumerator WaitForText()
    {
        UIText = GameObject.Find("/MainPlayer/MainGame Canvas/UI/Difficulty/").GetComponent<Text>();
        if (UIText == null)
        {
            yield return null;
        }
        else
        {
            UIText = GameObject.Find("/MainPlayer/MainGame Canvas/UI/Difficulty/").GetComponent<Text>();
        }
    }
    //======================================PositionEnumerators==============================//
    IEnumerator WaitForLevel1Position()
    {
        Level1StartPosition = GameObject.Find("/Level1StartPosition/");
        if (Level1StartPosition == null)
        {
            yield return null;
        }
        else
        {
            Level1StartPosition = GameObject.Find("/Level1StartPosition/");
            Position = Level2StartPosition.transform.position;
            gameObject.transform.position = Position;
            FoundPosition = true;
            AddLevelCounter = false;
        }
    }
    IEnumerator WaitForLevel2Position()
    {
        Level2StartPosition = GameObject.Find("/Level2StartPosition/");
        if (Level2StartPosition == null)
        {
            yield return null;
        }
        else
        {
            Level2StartPosition = GameObject.Find("/Level2StartPosition/");
            Position = Level2StartPosition.transform.position;
            gameObject.transform.position = Position;
            FoundPosition = true;
            AddLevelCounter = false;
        }

    }
    IEnumerator WaitForLevel3Position()
    {
        Level3StartPosition = GameObject.Find("/Level3StartPosition/");
        if (Level3StartPosition == null)
        {
            yield return null;
        }
        else
        {
            Level3StartPosition = GameObject.Find("/Level3StartPosition/");
            Position = Level3StartPosition.transform.position;
            gameObject.transform.position = Position;
            FoundPosition = true;
            AddLevelCounter = false;
        }

    }
    IEnumerator WaitForLevel4Position()
    {
        Level4StartPosition = GameObject.Find("/Level4StartPosition/");
        if (Level4StartPosition == null)
        {
            yield return null;
        }
        else
        {
            Level4StartPosition = GameObject.Find("/Level4StartPosition/");
            Position = Level4StartPosition.transform.position;
            gameObject.transform.position = Position;
            FoundPosition = true;
            AddLevelCounter = false;
        }

    }
    IEnumerator WaitForLevel5Position()
    {
        Level5StartPosition = GameObject.Find("/Level5StartPosition/");
        if (Level5StartPosition == null)
        {
            yield return null;
        }
        else
        {
            Level5StartPosition = GameObject.Find("/Level5StartPosition/");
            Position = Level5StartPosition.transform.position;
            gameObject.transform.position = Position;
            FoundPosition = true;
            AddLevelCounter = false;
        }

    }

}

