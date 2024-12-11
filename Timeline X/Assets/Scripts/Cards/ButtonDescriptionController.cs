using DG.Tweening;
using UnityEngine;

public class ButtonDescriptionController : MonoBehaviour
{
    [SerializeField] private CardController cardController;

    private void OnMouseDown()
    {
        Debug.Log("Estoy haciendo zoom");

        // Si el canvas de descripción está activo, lo ocultamos
        if (UIManager.GetCanvasDescription())
        {
            UIManager.HideDescription();
        }
        else
        {
            UIManager.PutTextDescription(cardController.ObtenerCardInfo().CardDescription);
            UIManager.ShowDescription(cardController.ObtenerPosicionCarta());
            UIManager.HideSpecificGroup(); // Llamamos a la función HideSpecificGroup
        }
    }
}