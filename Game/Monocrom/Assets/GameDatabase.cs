using UnityEngine;

[System.Serializable]
public class PlayerProgress
{
    public int saveSlot;
    public string saveName;
}

public class GameDatabase : MonoBehaviour
{
    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/save.json";
    }

    public void SaveGame(PlayerProgress progress, PlayerState state)
    {
        Save save = new Save(progress, state);
        string json = JsonUtility.ToJson(save);
        System.IO.File.WriteAllText(filePath, json);
    }

    public void LoadGame()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            Save save = JsonUtility.FromJson<Save>(json);
            // Load the game
        }
    }
    public void SavePlayerState(PlayerState state, Save save)
    {
        save.playerState = state;
        string json = JsonUtility.ToJson(save);
        System.IO.File.WriteAllText(filePath, json);
    }
    public PlayerState LoadPlayerState()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            Save save = JsonUtility.FromJson<Save>(json);
            return save.playerState;
        }
        else
        {
            return null;
        }
    }
    public void SavePlayerProgress(PlayerProgress progress, Save save)
    {
        save.playerProgress = progress;
        string json = JsonUtility.ToJson(save);
        System.IO.File.WriteAllText(filePath, json);
    }

    public PlayerProgress LoadPlayerProgress()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            Save save = JsonUtility.FromJson<Save>(json);
            return save.playerProgress;
        }
        else
        {
            return null;
        }
    }
}