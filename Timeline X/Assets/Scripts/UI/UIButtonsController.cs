using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonsController : MonoBehaviour
{

    [SerializeField] private string sceneGame;

    [SerializeField] private string sceneMainMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
