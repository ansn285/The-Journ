using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    public int damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hostile" && collision.GetComponent<EnemyAI>())
        {
            collision.GetComponent<EnemyAI>().DealDamage(damage);
        }
    }

}
