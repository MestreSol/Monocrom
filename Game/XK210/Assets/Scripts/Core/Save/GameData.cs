
using System;

[Serializable]
public class GameData{
    public Player playerState;
    public PlayerProgress progress;
    public GameData(){
        playerState = new Player();
        progress = new PlayerProgress();
    }
}