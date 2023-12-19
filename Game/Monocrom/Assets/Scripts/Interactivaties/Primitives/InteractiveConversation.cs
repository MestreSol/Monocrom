
using UnityEngine;

public class InteractiveConversation : MonoBehaviour, IInteractive
{
    public DialogueManager DialogueManager;
    public Dialogue sequence;
    public void Compleat()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<PlayerController>().ShowMessage("Interagir");
        }
    }
    public void OnTriggerStay2D(Collider2D collision){
        if(collision.tag.Equals("Player") && Input.GetKeyDown(KeyCode.E))
        {
            collision.GetComponent<PlayerController>().HideMessage();
            DialogueManager.StartDialogue(sequence);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<PlayerController>().HideMessage();
        }
    }
}