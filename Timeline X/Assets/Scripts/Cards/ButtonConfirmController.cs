using UnityEngine;

public class ButtonConfirmController : MonoBehaviour
{
    [SerializeField] private CardController cardController; 
    [SerializeField] private RoundManager roundManager; 

    private void OnEnable()
    {
        RoundManager.OnTurnChanged += UpdateButtonState; 
    }

    private void OnDisable()
    {
        RoundManager.OnTurnChanged -= UpdateButtonState; 
    }

    private void OnMouseDown()
    {
        Debug.Log("Carta confirmada");
        cardController.ConfirmarCartaTimeline();

        
        roundManager.ConfirmPlay();
    }

    private void UpdateButtonState(int currentPlayer, int currentRound)
    {
        
        Debug.Log($"Botón actualizado para el jugador {currentPlayer + 1} en la ronda {currentRound}");
    }
}