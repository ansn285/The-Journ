using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //    public void NewGame()
    //    {
    //        gameObject.GetComponent<Canvas>().enabled = false;
    //        GameObject.Find("/OpeningCutscene/Opening_Cutscene").GetComponent<PlayableDirector>().Play();
    //        Invoke("SceneChange", 9.16f);
    //    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void Load()
    {
        print("load");
    }

    public void Save()
    {
        print("save");
    }

    public void Settings()
    {
        print("settings");
    }

}
