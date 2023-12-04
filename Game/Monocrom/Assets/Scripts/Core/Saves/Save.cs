using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
[Serializable]
public class Player
{
    public Colors CurColor = Colors.WHITE;

    public int head = 0;
    public int hair = 0;
    public int body = 0;
    public int legs = 0;
    public int Accessory = 0;
    public int Preset = 0;

    public float Life = 0;
    public float Mana = 0;
    
    public int Forca = 0;
    public int Destresa = 0;
    public int Sorte = 0;
    public int Persepcao = 0;
    public int Constituicao = 0;
    public int Inteligencia = 0;
    public int Carisma = 0;
    public int Energia = 0;
}
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