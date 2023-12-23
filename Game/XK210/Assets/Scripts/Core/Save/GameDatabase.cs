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
    }
}