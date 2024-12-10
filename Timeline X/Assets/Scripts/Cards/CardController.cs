using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CardController : MonoBehaviour
{
    [SerializeField] private CardInfo cardInfo;

    [SerializeField] private CardInventory player;

    [SerializeField] private Transform handPlayer;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementEase;

    [SerializeField] private bool inTimeline = false;

    [SerializeField] private bool animationPlay = false;

    public void MoverCartaTimeline()
    {
        if (!inTimeline && !player.ObtenerIsCardMovement() && UIManager.GetActionDescription())
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
        if (inTimeline && !animationPlay && UIManager.GetActionDescription())
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
        if (inTimeline && UIManager.GetActionDescription())
        {
            int cantidadHijos = gameObject.transform.childCount;

            for (int i = 0; i < cantidadHijos - 1; i++)
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }
            gameObject.transform.parent = TimelineController.TimelineTransform();
            RoundManager.ConfirmPlay();
            player.ConfirmarCardMovement();
        }
    }

    public void AgregarHandPlayer(Transform gameObject)
    {
        handPlayer = gameObject;
    }

    public void AgregarCardInfo(CardInfo aux)
    {
        cardInfo = aux;
    }

    public void AgregarCardInvetory(CardInventory cardInventory)
    {
        player = cardInventory;
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
}
