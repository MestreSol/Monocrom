
using System;

[Serializable]
public class GameData{
    public Player player;
    public PlayerProgress progress;
    public GameData(){
        player = new Player();
        progress = new PlayerProgress();
    }
}