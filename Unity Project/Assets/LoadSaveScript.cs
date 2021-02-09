using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveScript : MonoBehaviour {

    public void SaveGame()
    {
        GameControl.control.Save();
    }
    public void LoadGame()
    {
        GameControl.control.Load();
    }
}
