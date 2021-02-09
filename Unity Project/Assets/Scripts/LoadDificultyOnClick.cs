using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDificultyOnClick : MonoBehaviour
{
    public static bool EasyMode;
    public static bool NormalMode;
    public static bool HardMode;

    public AudioSource audioSrc;
    public AudioClip MainMenuMusic;
    public GameObject LoadingScreenCanvas;
    public GameObject MainMenuCanvas;

    public void LoadEasyDifficulty(bool Easy)
    {
        audioSrc.clip = MainMenuMusic;
        audioSrc.Stop();
        EasyMode = true;
        MainMenuCanvas.SetActive(false);
        LoadingScreenCanvas.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
    }
    public void LoadNormalDifficulty(bool Normal)
    {
        audioSrc.clip = MainMenuMusic;
        audioSrc.Stop();
        NormalMode = true;
        MainMenuCanvas.SetActive(false);
        LoadingScreenCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
   
    }
    public void LoadHardDifficulty(bool Hard)
    {
        audioSrc.clip = MainMenuMusic;
        audioSrc.Stop();
        HardMode = true;
        MainMenuCanvas.SetActive(false);
        LoadingScreenCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;

    }

}


