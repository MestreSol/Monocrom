using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    // A save slot is a game object that has a save slot controller script attached to it.
    public GameObject GameObject;
    public ParticleSystem ParticleSystem;
    public ParticleSystem ParticleSystem2;
    public Save save;
    public TMP_Text SaveName;
    public TMP_Text SaveTime;
    public Animator anim;
    public int id;
    public void LoadCreateSave(GameObject obj)
    {
        if (save != null)
        {
            GameManager.instance.LoadScene(save.LastScene);
        }
        else
        {
            if (GameObject != null)
            {
                StartNewGame startNewGame = GameObject.GetComponent<StartNewGame>();
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
