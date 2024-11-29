using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private static RoundManager instance;

    [SerializeField] private GameObject jugador1;

    [SerializeField] private GameObject jugador2;

    public int totalPlayers = 2; 
    public int currentPlayer = 0; 
    public int currentRound = 1; 

    public delegate void TurnChanged(int player, int round);
    public static event TurnChanged OnTurnChanged;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        NotifyTurnChange(); 
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
        
        instance.NotifyTurnChange();
    }

    private void NotifyTurnChange()
    {
        Debug.Log($" {currentPlayer + 1}. Round {currentRound}");
        OnTurnChanged?.Invoke(currentPlayer, currentRound);
        switch(currentPlayer) {
            case 0: {
                jugador1.SetActive(true);
                jugador2.SetActive(false);
                break;
            }
            case 1: {
                jugador1.SetActive(false);
                jugador2.SetActive(true);
                break;
            }
        }
    }
}