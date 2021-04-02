using UnityEngine;

public class UndergroundLights : MonoBehaviour
{
    public GameObject lights;


    private void Start()
    {
        // When its morning in game then
        if (DNCycle.getCount() >= 6 && DNCycle.getCount() < 16)
        {
            lights.SetActive(false);
        }

        // Otherwise
        else
        {
            lights.SetActive(true);
        }
    }


}
