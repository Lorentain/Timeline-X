using DG.Tweening;
using UnityEngine;

public class ButtonDescriptionController : MonoBehaviour
{
    [SerializeField] private CardController cardController;

    private void OnMouseDown()
    {
        Debug.Log("Estoy haciendo zoom");

        
        if (UIManager.GetCanvasDescription())
        {
            UIManager.HideDescription();
        }
        else
        {
            UIManager.PutTextDescription(cardController.ObtenerCardInfo().CardDescription);
            UIManager.ShowDescription(cardController.ObtenerPosicionCarta());
            UIManager.HideSpecificGroup(); 
            UIManager.HideFeedSpecificGroup();
        }
    }
}