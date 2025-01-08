using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    [SerializeField] private DeckController deckController;

    [SerializeField] private GameObject handPlayer;

    [SerializeField] private List<GameObject> inventoryCard;

    [SerializeField] private GameObject prefabCard;

    [SerializeField] private int giveCardStart;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementeEase;

    [SerializeField] private bool isCardMovement = false;

    // Método para contar el número de cartas
    public int ContarCartas()
    {
        return inventoryCard.Count;
    }

    // Método que devuelve true si el inventario está vacío
    public bool EstaVacío()
    {
        return inventoryCard.Count == 0;
    }

    // Añadir cartas al inventario al inicio del juego
    public void AñadirCartasComienzo()
    {
        for (int i = 0; i < giveCardStart; i++)
        {
            CardInfo aux = deckController.RepartirCarta();
            Debug.Log(aux);
            CardController card = Instantiate(prefabCard, gameObject.transform).GetComponent<CardController>();
            card.AgregarCardInvetory(this);
            card.AgregarHandPlayer(handPlayer.transform);
            card.AgregarCardInfo(aux);
            inventoryCard.Add(card.gameObject);
            card.transform.localPosition = new Vector3(i - 1, 0, 0);
            ReordenarInventario();
        }
    }

    public void RobarCarta()
    {
        CardInfo aux = deckController.RepartirCarta();
        Debug.Log(aux);
        CardController card = Instantiate(prefabCard, gameObject.transform).GetComponent<CardController>();
        card.AgregarCardInvetory(this);
        card.AgregarHandPlayer(handPlayer.transform);
        card.AgregarCardInfo(aux);
        inventoryCard.Add(card.gameObject);
        card.transform.localPosition = new Vector3(inventoryCard.Count - 1 , 0, 0);
        ReordenarInventario();
        Debug.Log("Carta robada:" + aux.CardName);
    }

    public void MoverHaciaTimeline(GameObject card)
    {
        if (!isCardMovement)
        {
            inventoryCard.Remove(card);
            card.transform.parent = TimelineController.TimelineTransform();
            ReordenarInventario();
            isCardMovement = true;
        }
    }

    public void MoverHaciaInventario(GameObject card)
    {
        inventoryCard.Insert(inventoryCard.Count / 2, card);
        card.transform.parent = this.transform;
        ReordenarInventario();
        isCardMovement = false;
    }

    public void ReordenarInventario() // Formula i - (n/2 - 0.5)
    {
        float formula = (inventoryCard.Count / 2f) - 0.5f;
        for (int i = 0; i < inventoryCard.Count; i++)
        {
            inventoryCard[i].transform.DOLocalMoveX(i - formula, movementTime).SetEase(movementeEase);
        }
    }

    public bool ObtenerIsCardMovement()
    {
        return isCardMovement;
    }

    public void ConfirmarCardMovement()
    {
        isCardMovement = false;
    }

}
