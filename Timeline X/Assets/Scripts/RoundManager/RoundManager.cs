using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public int totalPlayers = 2; 
    public int currentPlayer = 0; 
    public int currentRound = 1; 

    public delegate void TurnChanged(int player, int round);
    public static event TurnChanged OnTurnChanged;

    void Start()
    {
        NotifyTurnChange(); 
    }

    public void ConfirmPlay()
    {
        Debug.Log($"Jugador {currentPlayer + 1} confirm� su jugada en la ronda {currentRound}");

        // Cambiar al siguiente jugador
        currentPlayer++;
        if (currentPlayer >= totalPlayers)
        {
            currentPlayer = 0;
            currentRound++;
            Debug.Log($"Comienza la ronda {currentRound}");
        }
        
        NotifyTurnChange();
    }

    private void NotifyTurnChange()
    {
        Debug.Log($" {currentPlayer + 1}. Round {currentRound}");
        OnTurnChanged?.Invoke(currentPlayer, currentRound);
    }
}