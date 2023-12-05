using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save
{
    public GameData gameData;
    public string saveName;
    public string TimeSave;
    public string Titulo;
    public string LastScene;
    public int saveSlot;

    public Save()
    {
        TimeSave = "00:00:00.000";
        gameData = new GameData();
        gameData.playerProgress = new PlayerProgress();
        gameData.playerState = new Player();
    }
    
}
[Serializable]
public class GameData
{
    public Player playerState;
    public PlayerProgress playerProgress;
}