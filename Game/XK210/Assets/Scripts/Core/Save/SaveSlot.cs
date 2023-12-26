using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    // A save slot is a game object that has a save slot controller script attached to it.
    public GameObject gameObject;
    public ParticleSystem particleSystem;
    public ParticleSystem particleSystem2;
    public Save save;
    public TMP_Text saveName;
    public TMP_Text saveTime;
    public Animator anim;
    public int id;
    public void LoadCreateSave(GameObject obj)
    {
        if (save != null)
        {
            GameManager.instance.LoadScene(save.lastScene);
        }
        else
        {
            if (gameObject != null)
            {
               StartNewGame startNewGame = gameObject.GetComponent<StartNewGame>();
                if (startNewGame != null)
                {
                    obj.GetComponent<Button>().onClick.AddListener(delegate { startNewGame.NewSave(id); });
                }
                else
                {
                    Debug.LogError("StartNewGame component not found on GameObject");
                }
            }
            else
            {
                Debug.LogError("GameObject is null");
            }
        }
        anim.SetInteger("SaveSlot", id);
        anim.SetTrigger("GoTo");
    }
}