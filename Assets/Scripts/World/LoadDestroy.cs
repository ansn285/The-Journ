using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDestroy : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Scene1")
        {
            Destroy(GameObject.Find("OpeningCutscene"));
        }
    }
}
