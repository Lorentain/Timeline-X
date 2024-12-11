using UnityEngine;
using UnityEngine.SceneManagement;  // Importa SceneManager

public class RestartButton : MonoBehaviour
{
    public void RestartGame()
    {
        // Carga la escena activa para reiniciar el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // M�todo para volver al men� principal
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Reemplaza "MainMenuSceneName" con el nombre de tu escena del men� principal
    }
}