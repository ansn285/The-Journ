using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    private Animator animator;
    private GameObject p;

    private void Start()
    {
        animator = transform.parent.gameObject.GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Hostile"))
        {
            if (Input.GetMouseButtonDown(1) && collision.GetComponent<RootObject>())
            {
                p = collision.GetComponent<RootObject>().root;
                p.GetComponent<Animator>().SetTrigger("Riposte");
                p.GetComponent<EnemyAI>().postureBroken = true;
            }

            if (Input.GetMouseButtonDown(0) && p.GetComponent<EnemyAI>().postureBroken)
            {
                StartCoroutine(p.GetComponent<EnemyAI>().RiposteHit());
                p.GetComponent<EnemyAI>().DealDamage(230);
            }
        }
    }
}
