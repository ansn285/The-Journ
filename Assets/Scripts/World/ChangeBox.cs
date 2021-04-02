using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBox : MonoBehaviour
{
    /*public Transform exitPoint;
    public CharacterMovement currPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == currPlayer.gameObject)
        {
            currPlayer.transform.position = exitPoint.position;
            SceneController.instance.ChangeScreenBox(exitPoint.transform.parent.GetComponent<ScreenBox>());
        }
    }

    private void OnDrawGizmos()
    {
        if(exitPoint == null) { return; }
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, exitPoint.position);
    }*/

    public GameObject box1;
    public GameObject box2;

    public Transform player;

    public float box1_x, box2_x;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (box1.activeSelf == true && box2.activeSelf == false)
            {
                box1.SetActive(false);
                box2.SetActive(true);
                player.position = new Vector3(box1_x, player.position.y, player.position.z);

                GameObject.Find("CM vcam1").GetComponent<ChangeConfinedBox>().ChangeConfiner("box2");
            }

            else if (box1.activeSelf == false && box2.activeSelf == true)
            {
                box1.SetActive(true);
                box2.SetActive(false);
                player.position = new Vector3(box2_x, player.position.y, player.position.z);

                GameObject.Find("CM vcam1").GetComponent<ChangeConfinedBox>().ChangeConfiner("box1");
            }
        }
    }

    

}
