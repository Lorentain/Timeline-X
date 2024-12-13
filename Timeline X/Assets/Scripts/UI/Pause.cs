using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; 
    private bool isPaused = false; 

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true); 
        Time.timeScale = 0f; 
        AudioListener.pause = true; 
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false); 
        Time.timeScale = 1f; 
        AudioListener.pause = false; 
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); 
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        pauseMenu.SetActive(false); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}