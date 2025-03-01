using UnityEngine;

public class ButtonHorizontalController : MonoBehaviour
{

    [SerializeField] private CardController cardController;
    [SerializeField] private string movementDirection;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && cardController.IsTimeline())
        {
            MoveCardToLeft();
        }

        if (Input.GetKeyDown(KeyCode.D) && cardController.IsTimeline())
        {
            MoveCardToRight();
        }
    }

    private void OnMouseDown()
    {
        MoveCardToRight();
        MoveCardToLeft();
    }

    private void MoveCardToRight()
    {
        if (movementDirection == "right" && cardController.IsTimeline() && !UIManager.GetAnimationDescriptionZoom() && !TimelineController.GetAnimationPlay())
        {
            TimelineController.MoverDerechaCartaTimeline(cardController.gameObject);
            Debug.Log("Derecha");
        }
    }

    private void MoveCardToLeft()
    {
        if (movementDirection == "left" && cardController.IsTimeline() && !UIManager.GetAnimationDescriptionZoom() && !TimelineController.GetAnimationPlay())
        {
            TimelineController.MoverIzquierdaCartaTimeline(cardController.gameObject);
        }
    }
}
