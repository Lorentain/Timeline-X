using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static int jugadorGanador;  // Variable estática para almacenar el ganador

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Ganador(int jugador)
    {
        // Guardar el jugador ganador en la variable estática
        jugadorGanador = jugador;

        // Lógica para mostrar que el jugador ha ganado
        Debug.Log($"¡El Jugador {jugador} ha ganado!");

        // Cargar la escena de victoria
        SceneManager.LoadScene("VictoryScene");  // Asegúrate de que el nombre coincida con la escena que creaste
    }
}