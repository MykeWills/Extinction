using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    public AudioMixer MainSound;


    public void SetSfxLvl(float sfxLvl)
    {
        MainSound.SetFloat("sfxVol", sfxLvl);
    }
    public void SetMusicLvl(float musicLvl)
    {
        MainSound.SetFloat("musicVol", musicLvl);
    }
    public void SetMasterLvl(float masterLvl)
    {
        MainSound.SetFloat("masterVol", masterLvl);
    }

}