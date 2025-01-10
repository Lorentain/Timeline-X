using DG.Tweening;
using UnityEngine;

public class ButtonDescriptionController : MonoBehaviour
{
    [SerializeField] private CardController cardController;

    [SerializeField] private bool isConfirmInTimeline;

    private void OnMouseDown()
    {
        Debug.Log("Estoy haciendo zoom");

        if (!TimelineController.GetAnimationPlay() && !UIManager.GetAnimationDescriptionZoom())
        {
            if (UIManager.GetCanvasDescription())
            {
                UIManager.HideDescription();
            }
            else
            {
                UIManager.PutTextDescription(cardController.ObtenerCardInfo().CardName, cardController.ObtenerCardInfo().CardDescription, isConfirmInTimeline ? cardController.ObtenerAñoCarta().ToString() : "????");
                UIManager.ShowDescription(cardController.ObtenerPosicionCarta());
                UIManager.HideSpecificGroup();
                UIManager.HideFeedSpecificGroup();
            }
        }
    }

    public void ConfirmInTimeline() {
        isConfirmInTimeline = true;
    }
}