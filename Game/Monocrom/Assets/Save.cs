using System.IO;
using UnityEngine;
public class PlayerState
{
    public Colors CurColor;

    public PlayerState()
    {
        CurColor = Colors.RED;
    }
}
public class Save
{
    public string saveName;
    public string TimeSave;
    public PlayerProgress playerProgress;
    public PlayerState playerState;

    public Save(PlayerProgress playerProgress, PlayerState playerState)
    {
        this.playerProgress = playerProgress;
        this.playerState = playerState;
    }
}