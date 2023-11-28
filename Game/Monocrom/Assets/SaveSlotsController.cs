using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Drawing;

public enum Colors
{
    RED,
    BLUE,
    YELLOW,
    WHITE,
    BLACK
}
public class SaveSlotsController : MonoBehaviour
{
    public SaveSlot[] saveSlots;

    private void Start()
    {
        LoadSaves();
    }

    private void LoadSaves()
    {
        for (int i = 0; i < saveSlots.Length; i++)
        {
            if (File.Exists(Application.persistentDataPath + "/save" + i + ".json"))
            {
                string json = File.ReadAllText(Application.persistentDataPath + "/save" + i + ".json");
                Save save = JsonUtility.FromJson<Save>(json);

                saveSlots[i].save = save;
                saveSlots[i].SaveName.text = save.playerProgress.saveName;
                saveSlots[i].SaveTime.text = save.TimeSave;

                UnityEngine.Color color = UnityEngine.Color.white; // Defina a cor aqui

                switch (saveSlots[i].save.playerState.CurColor)
                {
                    case Colors.RED:
                        color = new UnityEngine.Color(240 / 255f, 84 / 255f, 95 / 255f);
                        break;
                    case Colors.BLUE:
                        color = new UnityEngine.Color(108 / 255f, 193 / 255f, 240 / 255f);
                        break;
                    case Colors.YELLOW:
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

                var main = saveSlots[i].ParticleSystem.main;
                main.startColor = color;
                saveSlots[i].gameObject.GetComponent<Image>().color = color;
                saveSlots[i].SaveName.color = color;
                saveSlots[i].SaveTime.color = color;
            }
            else
            {
                saveSlots[i].save = null;
                saveSlots[i].SaveName.text = "Empty";
                saveSlots[i].SaveTime.text = "Empty";
                UnityEngine.Color color = UnityEngine.Color.white; // Defina a cor aqui

                var main = saveSlots[i].ParticleSystem.main;
                main.startColor = UnityEngine.Color.white;
                saveSlots[i].gameObject.GetComponent<Image>().color = color;
                saveSlots[i].SaveName.color = color;
                saveSlots[i].SaveTime.color = color;
            }
        }
    }

}
