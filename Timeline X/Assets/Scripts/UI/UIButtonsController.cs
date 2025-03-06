using System;
using System.Collections;
using UnityEngine;

public class UIButtonsController : MonoBehaviour
{

    [SerializeField] private string sceneGame;

    [SerializeField] private string sceneMainMenu;

    [SerializeField] private PauseController pauseController;

    [SerializeField] private String sceneStartName;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneStartName);  // Reemplaza "JuegoSceneName" con el nombre de tu escena del juego
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");  // Reemplaza "TutorialSceneName" con el nombre de tu escena del tutorial
    }

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

    public void ResumeGame()
    {
        pauseController.ResumeGame();
    }

    public void ChoosePlayer(int players)
    {
        PlayerPrefs.SetInt("TotalPlayers", players);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Para el modo Editor
#else
        Application.Quit();  // Para el modo ejecuci�n
#endif
    }

}
