using UnityEngine;

public class GameDatabase: MonoBehaviour
{
    private static string filePath;

    private void Start(){
        filePath = Application.persistentDataPath;
    }
    public void SaveGame(PlayerProgress progress, Player state, int slotID)
    {
        Save save = new Save();
        string json = JsonUtility.ToJson(save);
        System.IO.File.WriteAllText(filePath + "/save" + slotID + ".json", json);
    }

    public static Save CreateSave(string SaveID, string SaveName, string Titulo)
    {

        filePath = Application.persistentDataPath + "/save" + SaveID + ".json";
        if (!System.IO.File.Exists(filePath))
        {
            Save save = new();

            save.titulo = Titulo;
            save.gameData.player.state.color = Colors.WHITE;
            save.saveName = SaveName;
            save.saveSlot = int.Parse(SaveID);
            save.lastScene = "StartGame";

            string json = JsonUtility.ToJson(save);
            System.IO.File.WriteAllText(filePath, json);
            Debug.Log("Save Criado em: " + filePath);
            GameManager.instance.curSave = save;
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
            Player state = save.gameData.player;
            PlayerProgress progress = save.gameData.player.progress;
            GameManager.instance.curSave = save;
            GameManager.instance.LoadScene(save.lastScene);
            return save;
        }
        else
        {
            return null;
        }

    }
    public static void SavePlayerState(Player state, Save save)
    {
        save.gameData.player = state;
        string json = JsonUtility.ToJson(save);
        System.IO.File.WriteAllText(filePath, json);
    }
    public Player LoadPlayerState()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            Save save = JsonUtility.FromJson<Save>(json);
            return save.gameData.player;
        }
        else
        {
            return null;
        }
    }
    public void SavePlayerProgress(PlayerProgress progress, Save save)
    {
        save.gameData.player.progress = progress;
        string json = JsonUtility.ToJson(save);
        System.IO.File.WriteAllText(filePath, json);
    }

    public PlayerProgress LoadPlayerProgress()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            Save save = JsonUtility.FromJson<Save>(json);
            return save.gameData.player.progress;
        }
        else
        {
            return null;
        }
    }
}