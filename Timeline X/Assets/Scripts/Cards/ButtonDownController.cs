using UnityEngine;

public class ButtonDownController : MonoBehaviour
{

    [SerializeField] private CardController cardController;

    [SerializeField] private TimelineController timelineController;

    private void OnMouseDown() {
        if(!TimelineController.GetAnimationPlay())
        cardController.DevolverCartaAMano();
        Debug.Log("Hola");
    }

}
