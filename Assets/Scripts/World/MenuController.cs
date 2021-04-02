using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Canvas>().enabled)
        {
            GameObject.Find("Amie").GetComponent<CharacterController>().enabled = false;
        }
    }
}
