using UnityEngine;

public class ButtonConfirmController : MonoBehaviour
{
    [SerializeField] private CardController cardController; 

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
        //gameObject.transform.gameObject.GetComponent<ButtonDescriptionController>(); Terminar cambiar la variable "isConfirmInTimeline"
    }

    private void UpdateButtonState(int currentPlayer, int currentRound)
    {
        Debug.Log($"Bot�n actualizado para el jugador {currentPlayer + 1} en la ronda {currentRound}");
    }
}