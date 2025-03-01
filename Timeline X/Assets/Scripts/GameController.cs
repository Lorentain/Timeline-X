using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    // Referencia al canvas donde estan los inputs fields
    [SerializeField] private GameObject canvasChooseName;

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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
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
                    res = instance.newNamePlayer1;
                    break;
                }
            case "Jugador 2":
                {
                    res = instance.newNamePlayer2;
                    break;
                }
            case "Jugador 3":
                {
                    res = instance.newNamePlayer3;
                    break;
                }
            case "Jugador 4":
                {
                    res = instance.newNamePlayer4;
                    break;
                }
        }
        return res;
    }

    public static void GuardarNombreJugadores()
    {
        instance.newNamePlayer1 = instance.inputNamePlayer1.text.ToString();
        instance.newNamePlayer2 = instance.inputNamePlayer2.text.ToString();
        instance.newNamePlayer3 = instance.inputNamePlayer3.text.ToString();
        instance.newNamePlayer4 = instance.inputNamePlayer4.text.ToString();
    }

    public static void OcultarCanvasElegirNombres()
    {
        instance.canvasChooseName.SetActive(false);
    }

    public static void MostrarCanvasElegirNombres()
    {
        instance.canvasChooseName.SetActive(true);
    }
}