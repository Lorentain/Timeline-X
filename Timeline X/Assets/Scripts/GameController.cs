using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    // Referencia al canvas donde estan los inputs fields
    [SerializeField] private GameObject canvasChooseName;

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
        instance.newNamePlayer1 = GameObject.Find("Input Field Name P1").GetComponent<TMP_InputField>().text.ToString();
        instance.newNamePlayer2 = GameObject.Find("Input Field Name P2").GetComponent<TMP_InputField>().text.ToString();
        instance.newNamePlayer3 = GameObject.Find("Input Field Name P3").GetComponent<TMP_InputField>().text.ToString();
        instance.newNamePlayer4 = GameObject.Find("Input Field Name P4").GetComponent<TMP_InputField>().text.ToString();
    }
}