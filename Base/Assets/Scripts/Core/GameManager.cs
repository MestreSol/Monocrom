using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Playing,
    Paused,
    GameOver,
    Dialogue
}
public class GameManager : MonoBehaviour
{
    
    [SerializeField]public GameState curState { get; set; } = GameState.Playing;

    public static GameManager instance { get; private set; }

    public static Save curSave { get; set; }

    public static ConfigFromSave config { get; set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void StartGame()
    {
        curState = GameState.Playing;
    }

    public void PauseGame()
    {
        curState = GameState.Paused;
    }

    public void ResumeGame()
    {
        curState = GameState.Playing;
    }

    public void GameOver()
    {
        curState = GameState.GameOver;
    }

    public void RestartGame()
    {
        curState = GameState.Menu;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void EntreInDialgue(){
        curState = GameState.Dialogue;
    }
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void LoadSceneAdditive(string sceneName)
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

}