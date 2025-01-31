using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{

    [SerializeField] private List<CardInfo> listCards;

    private void Awake() {
        listCards = DBManager.GetCards();
    }

    private void Start()
    {
        foreach (CardInfo card in listCards)
        {
            Debug.Log(card.CardName);
        }
    }

    public CardInfo RepartirCarta()
    {
        CardInfo cardAux = null;

        if (listCards.Count != 0)
        {
            int index = Random.Range(0, listCards.Count);
            cardAux = listCards[index];
            listCards.RemoveAt(index);
        }
        else
        {
            GameController.Empate();
        }
        return cardAux;
    }
}
