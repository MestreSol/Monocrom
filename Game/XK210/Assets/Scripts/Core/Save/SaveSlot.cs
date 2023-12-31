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
    public StartNewGame startNewGame;
    public int id;
    public void LoadCreateSave(Button obj)
    {
        if (save != null)
        {
            GameManager.instance.LoadScene(save.lastScene);
        }
        else
        {
            if (gameObject != null)
            {
               
                if (startNewGame != null)
                {
                    obj.onClick.AddListener(delegate { startNewGame.NewSave(id); });
                    startNewGame.anim.SetInteger("SaveSlot", id);
                    startNewGame.anim.SetTrigger("GoTo");
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
    }
}