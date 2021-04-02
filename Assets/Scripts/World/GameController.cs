using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class GameController : MonoBehaviour
{

    public ParticleSystem rain;
    public Canvas inventoryCanvas;
    public Canvas pauseCanvas;
    public GameObject pauseMenuSelected;

    public static GameController controllerInstance;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (controllerInstance == null)
        {
            controllerInstance = this;
        }
        else if (controllerInstance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !inventoryCanvas.enabled && !pauseCanvas.enabled && SceneManager.GetActiveScene().name != "MainMenu")
        {
            Time.timeScale = 0;
            inventoryCanvas.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) ^ Input.GetKeyDown(KeyCode.I) && inventoryCanvas.enabled && !pauseCanvas.enabled)
        {
            Time.timeScale = 1;
            inventoryCanvas.enabled = false;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && !inventoryCanvas.enabled && !pauseCanvas.enabled && SceneManager.GetActiveScene().name != "MainMenu")
        {
            Time.timeScale = 0;
            pauseCanvas.enabled = true;
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(pauseMenuSelected);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !inventoryCanvas.enabled && pauseCanvas.enabled)
        {
            Time.timeScale = 1;
            pauseCanvas.enabled = false;
        }
        
    }
}
