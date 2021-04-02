using System.Collections;
using UnityEngine;

public class DNCycle : MonoBehaviour
{
    private static int count = 0;
    private bool caughtSystemTime = false;
    private Transform lightTransform;
    private int minutes = 0;

    private void Start()
    {
        count = 0;
        lightTransform = GameObject.Find("Directional Light").GetComponent<Transform>();
        InvokeRepeating("StartCycle", 0, 1);
    }

    private void FixedUpdate()
    {
        if (count == System.DateTime.Now.Hour)
        {
            caughtSystemTime = true;            
            StopAllCoroutines();
        }

        if (count >= 6 && count < 16 && !caughtSystemTime)
        {
            StopAllCoroutines();
            StartCoroutine(Sunrise());
        }

        if (count > 17 && !caughtSystemTime)
        {
            StopAllCoroutines();
            StartCoroutine(Sunset());
        }

        if (caughtSystemTime && minutes == 0 && lightTransform.rotation.eulerAngles.y > 0 && System.DateTime.Now.Hour < 12
            && System.DateTime.Now.Hour >= 6)
        {
            lightTransform.rotation = Quaternion.Euler(45, lightTransform.rotation.eulerAngles.y - (.25f * System.DateTime.Now.Minute), 0);
            minutes = 1;
        }

        if (caughtSystemTime && minutes == 0 && lightTransform.rotation.eulerAngles.y < 90 && System.DateTime.Now.Hour >= 17
            && System.DateTime.Now.Hour <= 22)
        {
            lightTransform.rotation = Quaternion.Euler(45, lightTransform.rotation.eulerAngles.y + (.6f * System.DateTime.Now.Minute), 0);
            minutes = 1;
        }

    }

    public static int getCount()
    {
        return count;
    }

    private void StartCycle()
    {

        if (!caughtSystemTime)
        {
            if (count == 23) { count = 0; }
            else { count++; }

        }
        

        if (caughtSystemTime && System.DateTime.Now.Hour >= 6 && System.DateTime.Now.Hour < 16
            && lightTransform.rotation.eulerAngles.y > 0)
        {
            if (System.DateTime.Now.Second == 0)
            {
                lightTransform.rotation = Quaternion.Euler(45, lightTransform.rotation.eulerAngles.y - 0.25f, 0);
            }
        }
        else if (caughtSystemTime && System.DateTime.Now.Hour >= 17 && System.DateTime.Now.Hour < 22
            && lightTransform.rotation.eulerAngles.y < 90)
        {
            if (System.DateTime.Now.Second == 0)
            {
                lightTransform.rotation = Quaternion.Euler(45, lightTransform.rotation.eulerAngles.y + 0.6f, 0);
            }
        }
    }


    /*private void FixedUpdate()
    {
        if (count >= 5 && count < 16)
        {
            StopAllCoroutines();
            StartCoroutine(Sunrise());
        }
        if (count > 17)
        {
            StopAllCoroutines();
            StartCoroutine(Sunset());
        }
    }

    void StartCycle()
    {
        if (count == 23) { count = 0; }
        else { count++; }
    }*/

    IEnumerator Sunrise()
    {
        if (lightTransform.rotation.y > 0)
        {
            lightTransform.rotation = Quaternion.Euler(45, lightTransform.rotation.eulerAngles.y - 0.4f, lightTransform.rotation.eulerAngles.z);
            yield return null;
        }
        else
        {
            lightTransform.rotation = Quaternion.Euler(45, 0, 0);
            StopAllCoroutines();
        }
    }

    IEnumerator Sunset()
    {
        if (lightTransform.rotation.eulerAngles.y < 90)
        {
            lightTransform.rotation = Quaternion.Euler(45, lightTransform.rotation.eulerAngles.y + 0.7f, lightTransform.rotation.eulerAngles.z);
            yield return null;
        }
        else
        {
            lightTransform.rotation = Quaternion.Euler(45, 90, 0);
            StopAllCoroutines();
        }
    }
}
