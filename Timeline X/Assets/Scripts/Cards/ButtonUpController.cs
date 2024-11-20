using UnityEngine;

public class ButtonController : MonoBehaviour
{

     [SerializeField] private CardController cardController;

    private void OnMouseDown() {
        cardController.AñadirCartaTimeline();
        Debug.Log("Adios");
    }
}
