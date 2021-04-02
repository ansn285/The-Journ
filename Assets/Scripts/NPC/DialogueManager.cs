using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject dialogueButton;
    public Text dialogueTextBox;
    public Dialogue dialogue;
    private Sentence currentDialogue;


    /// <summary>
    /// Starts the dialogue system.
    /// </summary>
    /// <param name="dialogue">Dialogue tree object that contains all of the dialogues along with the choices.</param>
    public void StartDialogue(Dialogue dialogue)
    {
        this.dialogue = dialogue;
        currentDialogue = this.dialogue.root;
        dialogueTextBox.text = currentDialogue.sentence;

        InitializeButtons();
    }

    private void InitializeButtons()
    {
        // Initiating choice buttons
        if (currentDialogue.children.Count > 0)
        {
            GameObject[] buttons = new GameObject[currentDialogue.children.Count];
            for (int i = 0; i < currentDialogue.children.Count; i++)
            {
                buttons[i] = Instantiate(dialogueButton);
                buttons[i].transform.SetParent(gameObject.transform);
                buttons[i].GetComponent<DialogueButton>().sentence = currentDialogue.children[i];
                buttons[i].GetComponent<Text>().text = currentDialogue.children[i].sentence;
            }
            StartCoroutine(SelectButton(buttons[0]));

            if (currentDialogue.children.Count == 2)
            {
                buttons[0].GetComponent<RectTransform>().localPosition = new Vector2(-421, 110);
                buttons[1].GetComponent<RectTransform>().localPosition = new Vector2(421, 110);

                // Setting onClick method for both buttons
                buttons[0].GetComponent<Button>().onClick.AddListener(() => NextDialogue(buttons[0], buttons[1]));
                buttons[1].GetComponent<Button>().onClick.AddListener(() => NextDialogue(buttons[1], buttons[0]));

                // Setting the navigation for both buttons
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnLeft = buttons[1].GetComponent<Button>();
                nav.selectOnRight = nav.selectOnLeft;
                buttons[0].GetComponent<Button>().navigation = nav;

                nav.selectOnLeft = buttons[0].GetComponent<Button>();
                nav.selectOnRight = nav.selectOnLeft;
                buttons[1].GetComponent<Button>().navigation = nav;

                var buttonSelect = buttons[0].GetComponent<Button>().navigation;
                buttonSelect.selectOnLeft = buttons[1].GetComponent<Button>();
                buttonSelect.selectOnRight = buttons[1].GetComponent<Button>();

                buttonSelect = buttons[1].GetComponent<Button>().navigation;
                buttonSelect.selectOnLeft = buttons[0].GetComponent<Button>();
                buttonSelect.selectOnRight = buttons[0].GetComponent<Button>();
            }

            else if (currentDialogue.children.Count == 1)
            {
                buttons[0].GetComponent<RectTransform>().localPosition = new Vector2(-421, 110);

                // Setting onClick method for both buttons
                buttons[0].GetComponent<Button>().onClick.AddListener(() => NextDialogue(buttons[0], null));

            }
        }

        // If no more choices then exit the dialogue
        else
        {
            var button = Instantiate(dialogueButton);
            button.GetComponent<Text>().text = "Good Bye";
            button.transform.SetParent(gameObject.transform);
            StartCoroutine(SelectButton(button));
            button.GetComponent<RectTransform>().localPosition = new Vector2(-421, 110);
            button.GetComponent<Button>().onClick.AddListener(() => EndDialogue(button));
        }
    }

    IEnumerator SelectButton(GameObject button)
    {
        eventSystem.SetSelectedGameObject(null);
        yield return null;
        eventSystem.SetSelectedGameObject(button);
    }

    /// <summary>
    /// Will choose the next dialogue from the selected choice.
    /// </summary>
    /// <param name="button">The same button object that was pressed.</param>
    private void NextDialogue(GameObject button, GameObject button2)
    {
        if (button.GetComponent<DialogueButton>().sentence.children.Count == 0)
        {
            EndDialogue(button);            
        }

        else
        {
            Sentence s = button.GetComponent<DialogueButton>().sentence;
            dialogueTextBox.text = s.children[0].sentence;
            currentDialogue = s.children[0];

            Destroy(button);

            InitializeButtons();
        }

        if (button2 != null)
        {
            Destroy(button2);
        }
    }

    /// <summary>
    /// End the dialogue with the NPC.
    /// </summary>
    /// <param name="button">The same button that was pressed.</param>
    private void EndDialogue(GameObject button)
    {
        GameObject.Find("GameUI/DialogueCanvas").GetComponent<Animator>().SetTrigger("Exit");
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().enabled = true;
        Destroy(button);
    }
}