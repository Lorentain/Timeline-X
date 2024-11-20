using UnityEngine;

public class ButtonDownController : MonoBehaviour
{

    [SerializeField] private CardController cardController;

    [SerializeField] private TimelineController timelineController;

    private void OnMouseDown() {
        cardController.DevolverCartaAMano();
        timelineController.EliminarCartaTimeline(transform.parent.gameObject);
        Debug.Log("Hola");
    }

}
