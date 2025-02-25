using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    // Referencia a cada input field del nombre de cada jugador
    [SerializeField] private TMP_InputField inputNamePlayer1;

    [SerializeField] private TMP_InputField inputNamePlayer2;

    [SerializeField] private TMP_InputField inputNamePlayer3;

    [SerializeField] private TMP_InputField inputNamePlayer4;

    // Variable de cada nombre nuevo asignado
    [SerializeField] private string newNamePlayer1;

    [SerializeField] private string newNamePlayer2;

    [SerializeField] private string newNamePlayer3;

    [SerializeField] private string newNamePlayer4;

    public static int jugadorGanador;  // Variable estática para almacenar el ganador

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else {
            Destroy(gameObject);
        }
    }

    public void Ganador(int jugador)
    {
        // Guardar el jugador ganador en la variable estática
        jugadorGanador = jugador;

        // Efecto de sonido al ganar la partida
        AudioManager.PlayFinishGameEffect();

        // Cargar la escena de victoria
        SceneManager.LoadScene("VictoryScene");  // Asegúrate de que el nombre coincida con la escena que creaste
    }

    public static void Empate()
    {
        Debug.Log("Partida empatada");
        jugadorGanador = -1;
        AudioManager.PlayFinishGameEffect();
        SceneManager.LoadScene("VictoryScene");
    }

    public static string CambiarNombreJugadores(string Player)
    {
        string res = null;
        switch (Player)
        {
            case "Jugador 1":
                {
                    res = Instance.newNamePlayer1;
                    break;
                }
            case "Jugador 2":
                {
                    res = Instance.newNamePlayer2;
                    break;
                }
            case "Jugador 3":
                {
                    res = Instance.newNamePlayer3;
                    break;
                }
            case "Jugador 4":
                {
                    res = Instance.newNamePlayer4;
                    break;
                }
        }
        return res;
    }

    public static void GuardarNombreJugadores()
    {
        Instance.newNamePlayer1 = Instance.inputNamePlayer1.text.ToString();
        Instance.newNamePlayer2 = Instance.inputNamePlayer2.text.ToString();
        Instance.newNamePlayer3 = Instance.inputNamePlayer3.text.ToString();
        Instance.newNamePlayer4 = Instance.inputNamePlayer4.text.ToString();
    }
}