using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState curState = GameState.Menu;
    public static GameManager instance;
    public Save curSave;
    public ConfigFromSave configure;

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

    public void EntreInDialogue()
    {
        curState = GameState.Dialog;
    }
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneAdditive(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}