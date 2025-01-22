using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private GameObject player1;

    [SerializeField] private GameObject player2;

    [SerializeField] private GameObject player3;

    [SerializeField] private GameObject player4;

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

        // Cargar la escena de victoria
        SceneManager.LoadScene("VictoryScene");  // Asegúrate de que el nombre coincida con la escena que creaste
    }

    public static void Empate() {
        Debug.Log("Partida empatada");
        jugadorGanador = -1;
        SceneManager.LoadScene("VictoryScene");
    }

    public static void CambiarNombreJugadores(string Player1, string Player2, string Player3, string Player4) {
        Instance.player1.name = Player1;
        Instance.player2.name = Player2;
        Instance.player3.name = Player3;
        Instance.player4.name = Player4;
    }
}