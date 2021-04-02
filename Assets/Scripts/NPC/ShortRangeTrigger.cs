using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeTrigger : MonoBehaviour
{
    public Enemy mainObject;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            mainObject.shortRange = true;
            mainObject.StartAttacking();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            mainObject.shortRange = false;
            mainObject.StopAttacking();
        }
    }
}
