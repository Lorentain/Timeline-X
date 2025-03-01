using UnityEngine;

public class ButtonController : MonoBehaviour
{

    [SerializeField] private CardController cardController;

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.W))
    //     {
    //         cardController.MoverCartaTimeline();
    //     }
    // }

    private void OnMouseDown()
    {
        cardController.MoverCartaTimeline();
        Debug.Log("Adios");
    }
}
