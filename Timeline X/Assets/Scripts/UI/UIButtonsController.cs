using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonsController : MonoBehaviour
{

    [SerializeField] private string sceneGame;

    [SerializeField] private string sceneMainMenu;

    [SerializeField] private PauseController pauseController;

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneGame);
    }
        public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(sceneMainMenu); 
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); 
    }

    public void ResumeGame() {
        pauseController.ResumeGame();
    }

}
