using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausingScript : MonoBehaviour {

    public static bool paused;
    public Text UIText;
    public AudioSource audioSrc;
    public AudioClip LevelMusic;
    public GameObject PauseMenu;
    public GameObject OptionsMenu;
    public GameObject SoundMenu;




    public float GameSpeed;
    public float MusicSpeed;



    // Use this for initialization
    void Start () {

        
        UIText.text = "";
        MusicSpeed = 1;
        GameSpeed = 1;
    }
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = GameSpeed;
        audioSrc.pitch = MusicSpeed;

        if (audioSrc.pitch >= 5 && Time.timeScale >= 5)
        {
            MusicSpeed = 5;
            GameSpeed = 5;
        }
        if (audioSrc.pitch <= 0 && Time.timeScale <= 0)
        {
            MusicSpeed = 0;
            GameSpeed = 0;
        }

        if (Input.GetButtonDown("Pause"))
        {

            paused = !paused;
            Time.timeScale = paused ? 0 : 1;
            if (paused == true)
            {
                PauseMenu.SetActive(true);
                GameSpeed = 0f;
                MusicSpeed = 0f;
                UIText.text = "Game Paused";
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                MusicSpeed = 1f;
                GameSpeed = 1f;
                UIText.text = "";
                PauseMenu.SetActive(false);
                OptionsMenu.SetActive(false);
                SoundMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }

        }
        if (Input.GetButtonDown("GameSpeedPositive"))
        {
            MusicSpeed += 0.1f * Time.deltaTime;
            GameSpeed += 0.1f * Time.deltaTime;
        }
        else if (Input.GetButtonDown("GameSpeedPositive"))
        {
            MusicSpeed -= 1.0f * Time.deltaTime;
            GameSpeed -= 1.0f * Time.deltaTime;
        }

    }
}
