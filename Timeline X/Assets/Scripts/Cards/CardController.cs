using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class CardController : MonoBehaviour
{
    [SerializeField] private CardInfo cardInfo;

    [SerializeField] private Transform handPlayer;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementEase;

    public void MoverCartaTimeline()
    {
        if(TimelineController.AñadirCartaTimeline(gameObject)) {
            gameObject.transform.DOMoveY(TimelineController.TimelinePosicionY(),movementTime).SetEase(movementEase);   
        }
    }

    public void DevolverCartaAMano()
    {
        gameObject.transform.DOMoveY(handPlayer.position.y,movementTime).SetEase(movementEase);
        TimelineController.EliminarCartaTimeline(gameObject);
    }

    public void ConfirmarCartaTimeline() {

        int cantidadHijos = gameObject.transform.childCount;

        for(int i = 0; i < cantidadHijos; i++) {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
}
