using UnityEngine;

public class ButtonConfirmController : MonoBehaviour
{

    [SerializeField] private CardController cardController;

    private void OnMouseDown() {
        Debug.Log("Carta confirmada");
        cardController.ConfirmarCartaTimeline();
    }
}
