using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    MainMenu,
    InGame,
    GameOver,
    Pause
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
   
    public bool CanProced()
    {
        return gameState == GameState.InGame;
    }
    public void PrintKeyPressed()
{
    if (Input.anyKeyDown)
    {
        Debug.Log("Key Pressed: " + Input.inputString);
    }
}
public void PauseGame()
{
    gameState = GameState.Pause;
    Time.timeScale = 0;
}
public void ResumeGame()
{
    gameState = GameState.InGame;
    Time.timeScale = 1;
}
public void GameOver()
{
    gameState = GameState.GameOver;
}
}

