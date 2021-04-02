using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FillWater : MonoBehaviour
{
    public PlayableDirector cutscene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && cutscene != null)
        {
            GameObject.Find("Amie").GetComponent<Animator>().SetFloat("velocityX", 0);
            GameObject.Find("Amie").GetComponent<Animator>().SetFloat("Run", 0);
            GameObject.Find("Amie").gameObject.transform.Find("bone_1").GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            
            GameObject.Find("Amie").GetComponent<PlayerStats>().items = new string[1];
            GameObject.Find("Amie").GetComponent<PlayerStats>().items[0] = "Water";
            cutscene.Play();
        }
    }
}
