using UnityEngine;

public class ButtonController : MonoBehaviour
{

    [SerializeField] private CardController cardController;

    private void OnMouseDown() {
        cardController.MoverCartaTimeline();
        Debug.Log("Adios");
    }
}
