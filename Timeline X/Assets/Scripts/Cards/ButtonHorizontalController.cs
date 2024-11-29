using UnityEngine;

public class ButtonHorizontalController : MonoBehaviour
{

    [SerializeField] private string movementDirection;

    private void OnMouseDown() {
        if(movementDirection == "right") {
            TimelineController.MoverDerechaCartaTimeline(transform.parent.gameObject);
            Debug.Log("Derecha");
        }

        if(movementDirection == "left") {
            TimelineController.MoverIzquierdaCartaTimeline(transform.parent.gameObject);
        }
    }
}
