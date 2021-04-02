using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAI : MonoBehaviour
{

    public GameObject enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.GetComponent<EnemyAI>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.GetComponent<EnemyAI>().enabled = false;
        }
    }

}
