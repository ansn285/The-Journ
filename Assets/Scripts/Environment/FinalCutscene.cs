using UnityEngine;
using UnityEngine.Playables;

public class FinalCutscene : MonoBehaviour
{
    public PlayableDirector cutscene;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cutscene.gameObject.SetActive(true);
            collision.GetComponent<Animator>().SetFloat("velocityX", 0);
            
            collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            collision.GetComponent<CharacterMovement>().enabled = false;
        }
    }
}
