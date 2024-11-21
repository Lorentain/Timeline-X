using UnityEngine;

public class ButtonHorizontalController : MonoBehaviour
{

    [SerializeField] private TimelineController timelineController;

    [SerializeField] private string movementDirection;

    private void OnMouseDown() {
        if(movementDirection == "right") {
            timelineController.MoverDerechaCartaTimeline(transform.parent.gameObject);
            Debug.Log("Derecha");
        }

        if(movementDirection == "left") {
            timelineController.MoverIzquierdaCartaTimeline(transform.parent.gameObject);
        }
    }
}
