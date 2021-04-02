using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class ChangeScene : MonoBehaviour
{
    public PlayableDirector fade_in;
    public string travelTo;
    public Vector3 position;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().SavePlayer();
            fade_in.Play();
        }
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void LoadScene(string scene)
    {
        if (scene != "" || scene != null)
        {
            SceneManager.LoadScene(scene);
        }

        else
        {
            SceneManager.LoadScene(travelTo);
        }
    }

}
