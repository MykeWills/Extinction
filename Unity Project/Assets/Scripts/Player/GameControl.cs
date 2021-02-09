using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public static GameControl control;

    public int SceneNumber;

    public GameObject LoadingScreenCanvas;
    public GameObject MainGameCanvas;
    public GameObject MainMenuCanvas;

    public GameObject Player;
    //public Vector3 lastCheckpoint;

    private bool CursorLocked;

    public float Sensitivity;
    public float GamepadadSensitivity;

    public Text ScoreText;
    public Text LivesText;
    public Text EnergyText;
    public Text AmmoText;
    public Text ShieldText;
    public Text MissileText;
    public Text DifficultyText;

    float FadeTime;
    float FadeSpeed = 1f;

    public float Shield;
    public float Energy;

    public int Score;
    public int Lives;
    int interval = 10000;
    int LifeCounter = 1;

    public int Missiles;
    public int Ammo;

    public bool Easy;
    public bool Normal;
    public bool Hard;

    bool StartMusic;
    public bool Invert;
    public bool EnabledGamepad;

    public bool LaserLevel1;
    public bool LaserLevel2;
    public bool LaserLevel3;
    public bool LaserLevel4;


    // Use this for initialization
    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        StartMusic = true;
        Cursor.lockState = CursorLockMode.None;
        MainMenuCanvas.SetActive(true);
        //lastCheckpoint = gameObject.transform.position;

    }
    void Update()
    {
        SceneNumber = SceneLoaderScript.scene;
        Easy = LoadDificultyOnClick.EasyMode;
        Normal = LoadDificultyOnClick.NormalMode;
        Hard = LoadDificultyOnClick.HardMode;
        Invert = LoadInvertOnClick.Invert;
        EnabledGamepad = SixDOFController.EnabledGamepad;
        Sensitivity = LookSensitivityOnSlide.Sensitivity;
        GamepadadSensitivity = GamepadSensitivityOnSlide.PadSensitivity;

        
        // Grab and Combine Score From Enemies on Tick
        Score = BombardierController.Score + DrillerEnemyController.Score + IonMinerController.Score + OmegaController.Score;
        Shield = PlayerDamage.Shield;
        Energy = PrimaryGunScript.Energy;
        Lives = PlayerDamage.Lives;
        Ammo = PrimaryGunScript.Ammo;
        Missiles = MissileScript.Missiles;

        LaserLevel1 = PrimaryGunScript.LaserLVLOne;
        LaserLevel2 = PrimaryGunScript.LaserLVLTwo;
        LaserLevel3 = PrimaryGunScript.LaserLVLThree;
        LaserLevel4 = PrimaryGunScript.LaserLVLFour;
 
        // Gain an Extra Life after Interval Number
        if (Score >= interval * LifeCounter)
        {
            ++LifeCounter;
            ++Lives;
            SetCountLives();
        }

        // Make Text go away after 5 Sec
        if (FadeTime > 0)
        {
            FadeTime -= Time.deltaTime * FadeSpeed;
        }

        if (FadeTime < 0)
        {
            FadeTime = 0;
            DifficultyText.text = "";
        }

        if (FadeTime >= 5)
        {
            FadeTime = 5;
        }
        outOfLock();
        SetCountScore();
    }

    // Set The Score to the UI Text
    void SetCountScore()
    {
        ScoreText.text = Score.ToString();
    }
    // Set The Lives to the UI Text
    void SetCountLives()
    {
        LivesText.text = Lives.ToString();
    }
    // Set The Laser Energy to the UI Text
    void SetCountEnergy()
    {
        EnergyText.text = Mathf.CeilToInt(Energy).ToString();
    }
    // Set The Gauss Ammo to the UI Text
    void SetCountAmmo()
    {
        AmmoText.text = Ammo.ToString();
    }
    // Set The Shield to the UI Text
    void SetCountShield()
    {
        ShieldText.text = Mathf.CeilToInt(Shield).ToString();
    }
    // Set The Missiles to the UI Text
    void SetCountMissiles()
    {
        MissileText.text = Missiles.ToString();
    }


    // Save Your Progress
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerSaveData.dat");

        PlayerData data = new PlayerData();

        SceneNumber = SceneManager.GetActiveScene().buildIndex;
        //lastCheckpoint = gameObject.transform.position;

        data.Shield = Shield;
        data.Energy = Energy;
        data.Score = Score;
        data.Missiles = Missiles;
        data.Ammo = Ammo;
        data.Lives = Lives;
        data.Easy = Easy;
        data.Normal = Normal;
        data.Hard = Hard;
        data.LaserLevel1 = LaserLevel1;
        data.LaserLevel2 = LaserLevel2;
        data.LaserLevel3 = LaserLevel3;
        data.LaserLevel4 = LaserLevel4;
        data.Invert = Invert;
        data.EnabledGamepad = EnabledGamepad;
        data.Sensitivity = Sensitivity;
        data.GamepadadSensitivity = GamepadadSensitivity;
        data.SceneToLoad = SceneNumber;
        //data.lastCheckpoint = lastCheckpoint;

        bf.Serialize(file, data);
        file.Close();
    }

    // Load Your Progress
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerSaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerSaveData.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            Shield = data.Shield;
            Energy = data.Energy;
            Score = data.Score;
            Missiles = data.Missiles;
            Ammo = data.Ammo;
            Lives = data.Lives;
            Easy = data.Easy;
            Normal = data.Normal;
            Hard = data.Hard;
            LaserLevel1 = data.LaserLevel1;
            LaserLevel2 = data.LaserLevel2;
            LaserLevel3 = data.LaserLevel3;
            LaserLevel4 = data.LaserLevel4;
            Invert = data.Invert;
            EnabledGamepad = data.EnabledGamepad;
            Sensitivity = data.Sensitivity;
            GamepadadSensitivity = data.GamepadadSensitivity;
            //lastCheckpoint = data.lastCheckpoint;
            SceneNumber = data.SceneToLoad;

            SceneManager.LoadScene(SceneNumber);
            //gameObject.transform.position = lastCheckpoint;

        }
    }
    private void outOfLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CursorLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                CursorLocked = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                CursorLocked = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

}

[Serializable]

class PlayerData
{
    public float Shield;
    public float Energy;
    public int Score;
    public int Missiles;
    public int Ammo;
    public int Lives;

    public bool Easy;
    public bool Normal;
    public bool Hard;

    public bool LaserLevel1;
    public bool LaserLevel2;
    public bool LaserLevel3;
    public bool LaserLevel4;

    public bool Invert;
    public bool EnabledGamepad;
    public float Sensitivity;
    public float GamepadadSensitivity;
    public int SceneToLoad;
    //public Vector3 lastCheckpoint;


}
