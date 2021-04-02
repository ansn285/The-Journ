using UnityEngine;
using UnityEngine.Playables;

public class StopPlayerMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayableDirector>().state.ToString() == "Playing")
        {

            GameObject.Find("Amie").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            GameObject.Find("Amie").GetComponent<CharacterMovement>().enabled = false;
        }

        if (GetComponent<PlayableDirector>().state.ToString() == "Paused")
        {
            GameObject.Find("Amie").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GameObject.Find("Amie").GetComponent<CharacterMovement>().enabled = true;
        }
    }
}
