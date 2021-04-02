using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SceneController : MonoBehaviour
{

    public string[] state1;
    public float[] state2;
    public bool[] state3;
    public Vector3[] state4;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Scene1" && GlobalStats.scene1Visited)
        {
            Destroy(GameObject.Find("Mother"));
            Destroy(GameObject.Find("OpeningCutscene"));
        }
    }

    public void Save()
    {

    }

    public void Load()
    {

    }

}


[Serializable]
class SceneData
{
    //public string[] state1;
    //public float[] state2;
    //public bool[] state3;
    //public Vector3[] state4;
}