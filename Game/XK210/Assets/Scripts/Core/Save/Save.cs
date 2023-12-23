using System;
public class Save 
{
    public GameData gameData;
    public string saveName;
    public string timeSave;
    public string titulo;
    public string lastScene;
    public int saveSlot;

    public Save()
    {
        timeSave = DateTime.Now.ToString();
        gameData = new GameData();
    }

}