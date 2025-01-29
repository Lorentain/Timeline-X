using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{

    [SerializeField] private List<CardInfo> listCards;

    [SerializeField] private List<CardInfo> cards;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cards = DBManager.GetCards();
            foreach (CardInfo card in cards)
            {
                Debug.Log(card.CardName);
            }
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
