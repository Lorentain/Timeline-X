using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class CardController : MonoBehaviour
{
    [SerializeField] private CardInfo cardInfo;

    [SerializeField] private CardInventory player;

    [SerializeField] private Transform handPlayer;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementEase;

    public void MoverCartaTimeline()
    {
        if (TimelineController.AñadirCartaTimeline(gameObject))
        {
            gameObject.transform.DOMove(TimelineController.TimelinePosicion(), movementTime).SetEase(movementEase);
            player.MoverCartasInventario(transform.gameObject);
            Debug.Log("Carta correctamente colocada en el timeline");
        }
        else
        {
            Debug.Log("Carta colocada incorrectamente, regresa a la mano");
            DevolverCartaAMano();
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
        gameObject.transform.parent = TimelineController.TimelineTransform();
        RoundManager.ConfirmPlay();
    }

    public void AgregarHandPlayer(Transform gameObject) {
        handPlayer = gameObject;
    }

    public void AgregarCardInfo(CardInfo aux) {
        cardInfo = aux;
    }

    public void AgregarCardInvetory(CardInventory cardInventory) {
        player = cardInventory;
    }

    public CardInfo ObtenerCardInfo() {
        return cardInfo;
    }

    public Vector3 ObtenerPosicionCarta() {
        return gameObject.transform.position;
    }
}
