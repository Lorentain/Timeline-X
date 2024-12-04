using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    [SerializeField] private DeckController deckController;

    [SerializeField] private GameObject handPlayer;

    [SerializeField] private List<GameObject> inventoryCard;

    [SerializeField] private GameObject prefabCard;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementeEase;

    public void AñadirCartasComienzo()
    {
        for (int i = 0; i < 4; i++)
        {
            CardInfo aux = deckController.RepartirCarta();
            Debug.Log(aux);
            CardController card = Instantiate(prefabCard, gameObject.transform).GetComponent<CardController>();
            card.AgregarCardInvetory(this);
            card.AgregarHandPlayer(handPlayer.transform);
            card.AgregarCardInfo(aux);
            inventoryCard.Add(card.gameObject);
            card.transform.localPosition = new Vector3(i - 1, 0, 0);
            ReordenarInvetario();
        }
    }

    public void MoverHaciaTimeline(GameObject card)
    {
        inventoryCard.Remove(card);
        card.transform.parent = TimelineController.TimelineTransform();
        ReordenarInvetario();
    }

    public void MoverHaciaInventario(GameObject card) {
        inventoryCard.Insert(inventoryCard.Count/2,card);
        card.transform.parent = this.transform;
        ReordenarInvetario();
    }

    public void ReordenarInvetario() { // Formula i - (n/2 - 0.5)
        float formula = (inventoryCard.Count/2f) - 0.5f;
        for(int i = 0; i < inventoryCard.Count; i++) {
            inventoryCard[i].transform.DOLocalMoveX(i - formula,movementTime).SetEase(movementeEase);
        }
    }

}
