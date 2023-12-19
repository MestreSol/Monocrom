using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject DialogueBox;
    public Dialogue[] Sequence;
    public List<DialogueOptions> Options;
    private Queue<string> sentences;

    // Singleton instance
    public static DialogueManager Instance { get; private set; }

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        sentences = new Queue<string>();
    }
    void Update()
    {
        if(GameManager.instance.curState == GameState.Dialogue){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DisplayNextSentence();
            }
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        GameManager.instance.curState = GameState.Dialogue;
        // Get Firts position of Sequence
        DialogueBox.SetActive(true);
        nameText.text = dialogue.people.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void StartDialogue(Dialogue dialogue, DialogueOptions[] options)
    {
        GameManager.instance.curState = GameState.Dialogue;
        // Get Firts position of Sequence
        DialogueBox.SetActive(true);
        nameText.text = dialogue.people.name;
        Options = options.ToList();
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

    }
    void ShowOptions(){
        if(Options.Count > 0){
            foreach (var option in Options)
            {
                // option.Action.Invoke();
            }
        }
    }
    void EndDialogue()
    {
        if(Options.Count <= 0){
            DialogueBox.SetActive(false);
            GameManager.instance.curState = GameState.Playing;
        }
        else{
            ShowOptions();
        }
    }
}