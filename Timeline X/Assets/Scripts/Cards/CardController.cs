using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class CardController : MonoBehaviour
{

    [SerializeField] private Transform timeline;

    [SerializeField] private Transform handPlayer;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementEase;

    public void AñadirCartaTimeline()
    {
        gameObject.transform.DOMoveY(timeline.position.y,movementTime).SetEase(movementEase);
    }

    public void DevolverCartaAMano()
    {
        gameObject.transform.DOMoveY(handPlayer.position.y,movementTime).SetEase(movementEase);
    }
}
