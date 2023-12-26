using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Drawing;
public class SaveSlotsController : MonoBehaviour
{
    public SaveSlot[] saveSlots;
    public Animator animator;

    private void Start()
    {
        GetSavesInGameObj();
        LoadSaves();
        animator.SetTrigger("Start");
    }

    private void GetSavesInGameObj()
    {
        saveSlots = gameObject.GetComponentsInChildren<SaveSlot>();
    }
    private void LoadSaves()
    {
        Debug.Log("Loading Saves in path: " + Application.persistentDataPath);
        for (int i = 0; i < saveSlots.Length; i++)
        {
            Debug.Log("Checking Save " + i);
            if (File.Exists(Application.persistentDataPath + "/save" + i + ".json"))
            {
                Debug.Log("Save " + i + " Exists");
                string json = File.ReadAllText(Application.persistentDataPath + "/save" + i + ".json");
                Save save = JsonUtility.FromJson<Save>(json);

                saveSlots[i].save = save;
                saveSlots[i].saveName.text = save.saveName;
                saveSlots[i].saveTime.text = save.timeSave;

                UnityEngine.Color color = UnityEngine.Color.white; // Defina a cor aqui

                switch (saveSlots[i].save.gameData.player.state.color)
                {
                    case Colors.RED:
                        color = new UnityEngine.Color(240 / 255f, 84 / 255f, 95 / 255f);
                        break;
                    case Colors.BLUE:
                        color = new UnityEngine.Color(108 / 255f, 193 / 255f, 240 / 255f);
                        break;
                    case Colors.GREEN:
                        color = new UnityEngine.Color(240 / 255f, 236 / 255f, 84 / 255f);
                        break;
                    case Colors.WHITE:
                        color = UnityEngine.Color.white;
                        break;
                    case Colors.BLACK:
                        color = UnityEngine.Color.black;
                        break;
                    default:
                        color = UnityEngine.Color.white;
                        break;
                }

                var main = saveSlots[i].particleSystem.main;
                main.startColor = color;
                saveSlots[i].gameObject.GetComponent<Image>().color = color;
                saveSlots[i].saveName.color = color;
                saveSlots[i].saveTime.color = color;

            }
            else
            {
                saveSlots[i].save = null;
                saveSlots[i].saveName.text = "Empty";
                saveSlots[i].saveTime.text = "Empty";
                UnityEngine.Color color = UnityEngine.Color.white; // Defina a cor aqui

                var main = saveSlots[i].particleSystem.main;
                main.startColor = UnityEngine.Color.white;
                saveSlots[i].gameObject.GetComponent<Image>().color = color;
                saveSlots[i].saveName.color = color;
                saveSlots[i].saveTime.color = color;
            }
        }
    }

}
