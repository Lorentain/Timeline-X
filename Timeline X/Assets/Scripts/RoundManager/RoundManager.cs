using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private static RoundManager instance;

    [SerializeField] private GameObject jugador1;
    [SerializeField] private GameObject jugador2;

    [SerializeField] private CardInventory cardInventoryPlayer1;
    [SerializeField] private CardInventory cardInventoryPlayer2;

    public int totalPlayers = 2;
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
        // Añadir cartas al inicio para cada jugador
        cardInventoryPlayer1.AñadirCartasComienzo();
        cardInventoryPlayer2.AñadirCartasComienzo();

        NotifyTurnChange();  // Comienza el turno después de repartir las cartas
    }

    public static void ConfirmPlay()
    {
        Debug.Log($"Jugador {instance.currentPlayer + 1} confirma su jugada en la ronda {instance.currentRound}");

        // Cambiar al siguiente jugador
        instance.currentPlayer++;
        if (instance.currentPlayer >= instance.totalPlayers)
        {
            instance.currentPlayer = 0;
            instance.currentRound++;
            Debug.Log($"Comienza la ronda {instance.currentRound}");
        }

        // Comprobar si algún jugador se ha quedado sin cartas
        if (instance.cardInventoryPlayer1.ContarCartas() == 0) // Verifica si jugador 1 tiene 0 cartas
        {
            GameController.Instance.Ganador(1); // Jugador 1 ha ganado
        }
        else if (instance.cardInventoryPlayer2.ContarCartas() == 0) // Verifica si jugador 2 tiene 0 cartas
        {
            GameController.Instance.Ganador(2); // Jugador 2 ha ganado
        }

        instance.NotifyTurnChange();
    }

    private void NotifyTurnChange()
    {
        Debug.Log($" {currentPlayer + 1}. Round {currentRound}");
        OnTurnChanged?.Invoke(currentPlayer, currentRound);
        switch (currentPlayer)
        {
            case 0:
                jugador1.SetActive(true);
                jugador2.SetActive(false);
                break;
            case 1:
                jugador1.SetActive(false);
                jugador2.SetActive(true);
                break;
        }
    }
}