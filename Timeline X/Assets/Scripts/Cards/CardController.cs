using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CardController : MonoBehaviour
{
    [SerializeField] private CardInfo cardInfo;

    [SerializeField] private CardInventory player;

    [SerializeField] private Transform handPlayer;

    [SerializeField] private SpriteRenderer spriteRendererImagen;

    [SerializeField] private GameObject buttonToDestroy;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementEase;

    [SerializeField] private bool inTimeline = false;

    [SerializeField] private bool animationPlay = false;

    public void MoverCartaTimeline()
    {

        if (!inTimeline && !player.ObtenerIsCardMovement() && !UIManager.GetActiveDescription())
        {
            if (TimelineController.AñadirCartaTimeline(gameObject) && !animationPlay)
            {
                GetComponent<SortingGroup>().sortingOrder += 1;
                animationPlay = true;
                gameObject.transform.DOMove(TimelineController.TimelinePosicion(), movementTime).SetEase(movementEase).OnComplete(() => {
                    animationPlay = false;
                    GetComponent<SortingGroup>().sortingOrder -= 1;
                });
                player.MoverHaciaTimeline(transform.gameObject);
                inTimeline = true;
                Debug.Log("Movimiento de carta al timeline");
            }

        }
    }

    public void DevolverCartaAMano()
    {
        Debug.Log(inTimeline +  " " + animationPlay);
        if (inTimeline && !animationPlay && !UIManager.GetActiveDescription())
        {
            GetComponent<SortingGroup>().sortingOrder += 1;
            player.MoverHaciaInventario(transform.gameObject);
            animationPlay = true;
            gameObject.transform.DOLocalMoveY(0, movementTime).SetEase(movementEase).OnComplete(() => {
                animationPlay = false;
                GetComponent<SortingGroup>().sortingOrder -= 1;
            });
            TimelineController.EliminarCartaTimeline(gameObject);
            inTimeline = false;
        }
    }

    public void ConfirmarCartaTimeline()
    {
        if (inTimeline && !UIManager.GetActiveDescription())
        {
            gameObject.transform.parent = TimelineController.TimelineTransform();
            player.ConfirmarCardMovement();
            TimelineController.ComprobarCarta(gameObject);
            Destroy(buttonToDestroy);
        }
    }

    public void AgregarHandPlayer(Transform gameObject)
    {
        handPlayer = gameObject;
    }

    public void AgregarCardInfo(CardInfo aux)
    {
        cardInfo = aux;
        spriteRendererImagen.sprite = cardInfo.CardImage;
    }

    public void AgregarCardInvetory(CardInventory cardInventory)
    {
        player = cardInventory;
    }

    public CardInventory ObtenerInventario() {
        return player;
    }

    public CardInfo ObtenerCardInfo()
    {
        return cardInfo;
    }

    public Vector3 ObtenerPosicionCarta()
    {
        return gameObject.transform.position;
    }

    public bool IsTimeline()
    {
        return inTimeline;
    }

    public int ObtenerAñoCarta() {
        return cardInfo.CardDateYear;
    }
}
