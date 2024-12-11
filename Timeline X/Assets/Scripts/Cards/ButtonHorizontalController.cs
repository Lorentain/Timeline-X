using UnityEngine;

public class ButtonHorizontalController : MonoBehaviour
{

    [SerializeField] private CardController cardController;
    [SerializeField] private string movementDirection;

    private void OnMouseDown() {
        if(movementDirection == "right" && cardController.IsTimeline()) {
            TimelineController.MoverDerechaCartaTimeline(cardController.gameObject);
            Debug.Log("Derecha");
        }

        if(movementDirection == "left" && cardController.IsTimeline()) {
            TimelineController.MoverIzquierdaCartaTimeline(cardController.gameObject);
        }
    }
}
