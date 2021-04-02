using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a sentence object that takes in numerous sentences and the next sentence object 
/// that will come after the sentence array ends.
/// </summary>
/*[Serializable]
public class Sentence
{
    // Defining attributes
    public string[] sentences;
    [System.NonSerialized] public Sentence nextSet;
    public Sentence[] choices;

    private Sentence(string[] sentences, Sentence nextSet, Sentence[] choices)
    {
        this.sentences = sentences;
        this.nextSet = nextSet;
        this.choices = choices;
    }
}
*/


public class Sentence
{
    // Defining attributes
    public List<Sentence> children;
    public string sentence;
    public Sentence parent;
    public bool isLeaf;

    public Sentence(string sentence)
    {
        this.sentence = sentence;
        this.children = new List<Sentence>();
        this.parent = null;
    }

    public void SetParent(Sentence parent) => this.parent = parent;

    public void SetChildren(List<Sentence> children)
    {
        this.children = children;
        if (this.children.Count == 0)
        {
            isLeaf = true;
        }
        else
        {
            isLeaf = false;
        }
    }

}

public class Dialogue
{
    private List<Sentence> dialogue;
    public Sentence root;


    /// <summary>
    /// Takes an array of string as input that contains all of the dialogues and the options and creates 
    /// the Sentence objects of each string
    /// </summary>
    /// <param name="dialogue">String array of all of the dialogues and their choices.</param>
    public Dialogue(string[] dialogue)
    {
        // Creating Sentence object of all of the dialogues and storing them
        this.dialogue = new List<Sentence>();
        
        for (int i = 0; i < dialogue.Length; i++)
        {
            var node = new Sentence(dialogue[i]);
            this.dialogue.Add(node);
        }

        root = this.dialogue[0];
        CreateTree();
    }

    /// <summary>
    /// Creates relation between each node of the tree and links them so they can be used.
    /// </summary>
    private void CreateTree()
    {

        for (int i = 0; i < this.dialogue.Count; i++)
        {
            // Creating an arraylist and storing the potential children in it
            List<Sentence> children = new List<Sentence>();

            if (i * 2 + 1 < this.dialogue.Count && this.dialogue[i * 2 + 1].sentence != "")
            {
                this.dialogue[i * 2 + 1].SetParent(this.dialogue[i]);
                children.Add(this.dialogue[i * 2 + 1]);
            }
            if (i * 2 + 2 < this.dialogue.Count && this.dialogue[i * 2 + 2].sentence != "")
            {
                this.dialogue[i * 2 + 2].SetParent(this.dialogue[i]);
                children.Add(this.dialogue[i * 2 + 2]);
            }
            

            // Setting the children of the current node
            this.dialogue[i].SetChildren(children);
        }

    }

    private void RemoveEmptyNodes()
    {
        
    }

    /// <summary>
    /// Prints the whole tree that was created.
    /// </summary>
    public void PrintTree()
    {
        for (int i = 0; i < this.dialogue.Count; i++)
        {
            var a = "Node: " + this.dialogue[i].sentence + ", Children: [";

            if (dialogue[i].children.Count > 0)
            {
                if (dialogue[i].children.Count == 2)
                {
                    a += dialogue[i].children[0].sentence + ", " + dialogue[i].children[1].sentence;
                }
                else if (dialogue[i].children.Count == 1)
                {
                    a += dialogue[i].children[0].sentence;
                }
            }
            a += "]";

            Debug.Log(a);
        }
    }
}