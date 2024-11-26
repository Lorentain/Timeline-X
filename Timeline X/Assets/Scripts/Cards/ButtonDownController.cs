using UnityEngine;

public class ButtonDownController : MonoBehaviour
{

    [SerializeField] private CardController cardController;

    [SerializeField] private TimelineController timelineController;

    private void OnMouseDown() {
        cardController.DevolverCartaAMano();
        Debug.Log("Hola");
    }

}
