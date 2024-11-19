using UnityEngine;

public class ButtonDownController : MonoBehaviour
{

    [SerializeField] private CardController cardController;

    private void OnMouseDown() {
        cardController.DevolverCartaAMano();
        Debug.Log("Hola");
    }

}
