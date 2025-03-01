using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private static RoundManager instance;

    [SerializeField] private List<GameObject> listPlayers;

    [SerializeField] private List<CardInventory> listCardInventoryPlayers;

    [SerializeField] private List<PowerUpsInventory> listPowerUpsInventoryPlayers;

    [SerializeField] private ActionFeedManager actionFeedManager;  // Referencia al ActionFeedManager

    public int totalPlayers = 4;
    public int currentPlayer = 0;
    public int currentRound = 1;

    public delegate void TurnChanged(int player, int round);
    public static event TurnChanged OnTurnChanged;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        totalPlayers = PlayerPrefs.GetInt("TotalPlayers");
        // Añadir cartas al inicio para cada jugador
        for(int i = 0; i < totalPlayers;i++) {
            listCardInventoryPlayers[i].AñadirCartasComienzo();
            listPowerUpsInventoryPlayers[i].AñadirPowerUpsComienzo();

        }
        TimelineController.PonerCartaInicial();

        // Notificar el cambio de turno inicial
        NotifyTurnChange();  // Comienza el turno despues de repartir las cartas
    }

    public static void ConfirmPlay(bool correctCard)
    {

        // Registrar la acci�n en el feed y consola
        instance.actionFeedManager.LogAction($"Jugador {GameController.CambiarNombreJugadores("Jugador " + (instance.currentPlayer + 1))} confirma su jugada en la ronda {instance.currentRound}");
        instance.actionFeedManager.LogAction("Turno finalizado.");

        // Cambiar al siguiente jugador
        instance.currentPlayer++;
        if (instance.currentPlayer >= instance.totalPlayers)
        {
            instance.currentPlayer = 0;
            instance.currentRound++;

            // Registrar la acci�n en el feed y consola
            instance.actionFeedManager.LogAction($"Comienza la ronda {instance.currentRound}");
        }

        if (correctCard)
        {
            // Comprobar si algún jugador se ha quedado sin cartas
            for(int i = 0; i < instance.totalPlayers;i++) {
                if(instance.listCardInventoryPlayers[i].ContarCartas() == 0) { // Verifica si jugador X tiene 0 cartas
                    GameController.instance.Ganador(i+1); // Jugador X ha ganado
                    instance.actionFeedManager.LogAction(GameController.CambiarNombreJugadores("Jugador " + (i + 1)) + "ha ganado la partida, se quedó sin cartas."); // Registrar la acción en el feed y consola
                }
            }
        }
        NotifyTurnChange();
    }

    public static void NotifyTurnChange()
    {
        // Registrar el cambio de turno
        instance.actionFeedManager.LogAction($"Es el turno de {GameController.CambiarNombreJugadores("Jugador " + (instance.currentPlayer + 1))} - Ronda {instance.currentRound}");

        OnTurnChanged?.Invoke(instance.currentPlayer, instance.currentRound);
    }

    public static void ChangePlayer()
    {
        for(int i = 0; i < instance.totalPlayers;i++) {
            if(instance.currentPlayer == i) {
                instance.listPlayers[i].SetActive(true);
            }else {
                instance.listPlayers[i].SetActive(false);
            }
        }
    }
}