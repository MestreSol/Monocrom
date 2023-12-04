using UnityEngine;

[System.Serializable]
public class PlayerProgress
{
    public int Level = 0;
    public Colors Colors = Colors.WHITE;
}

public class GameDatabase : MonoBehaviour
{
    private static string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/save.json";
    }

    public void SaveGame(PlayerProgress progress, Player state)
    {
        Save save = new Save();
        string json = JsonUtility.ToJson(save);
        System.IO.File.WriteAllText(filePath, json);
    }

    public static Save CreateSave(string SaveID, string SaveName, string Titulo)
    {

        filePath = Application.persistentDataPath + "/save" + SaveID + ".json";
        if (!System.IO.File.Exists(filePath))
        {
            Save save = new();

            save.Titulo = Titulo;
            save.gameData.playerState.CurColor = Colors.WHITE;
            save.saveName = SaveName;
            save.saveSlot = int.Parse(SaveID);
            save.LastScene = "StartGame";

            string json = JsonUtility.ToJson(save);
            System.IO.File.WriteAllText(filePath, json);
            Debug.Log("Save Criado em: "+filePath);
            return save;
        }

        return LoadGame(SaveID);
    }
    public static Save LoadGame(string SaveID)
    {
        filePath = Application.persistentDataPath + "/save" + SaveID + ".json";
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            Save save = JsonUtility.FromJson<Save>(json);
            
            //Load Game info
            Player state = save.gameData.playerState;
            PlayerProgress progress = save.gameData.playerProgress;
            GameManager.instance.LoadScene(save.LastScene);
            return save;
        }
        else
        {
            return null;
        }

    }
    public static void SavePlayerState(Player state, Save save)
    {
        save.gameData.playerState = state;
        string json = JsonUtility.ToJson(save);
        System.IO.File.WriteAllText(filePath, json);
    }
    public Player LoadPlayerState()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            Save save = JsonUtility.FromJson<Save>(json);
            return save.gameData.playerState;
        }
        else
        {
            return null;
        }
    }
    public void SavePlayerProgress(PlayerProgress progress, Save save)
    {
        save.gameData.playerProgress = progress;
        string json = JsonUtility.ToJson(save);
        System.IO.File.WriteAllText(filePath, json);
    }

    public PlayerProgress LoadPlayerProgress()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            Save save = JsonUtility.FromJson<Save>(json);
            return save.gameData.playerProgress;
        }
        else
        {
            return null;
        }
    }
}