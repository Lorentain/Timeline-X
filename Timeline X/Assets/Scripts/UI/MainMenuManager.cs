using UnityEngine;
using UnityEngine.SceneManagement;  // Importa SceneManager

public class MainMenuManager : MonoBehaviour
{
    // M�todo para cargar la escena del juego
    public void StartGame()
    {
        SceneManager.LoadScene("Tanillo");  // Reemplaza "JuegoSceneName" con el nombre de tu escena del juego
    }

    // M�todo para cargar la escena del tutorial
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");  // Reemplaza "TutorialSceneName" con el nombre de tu escena del tutorial
    }

    // M�todo para salir del juego
    public void ExitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // Para el modo Editor
#else
        Application.Quit();  // Para el modo ejecuci�n
#endif
    }
}