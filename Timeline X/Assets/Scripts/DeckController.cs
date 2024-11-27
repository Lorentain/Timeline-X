using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{

    [SerializeField] private List<CardInfo> listCards;

    public CardInfo RepartirCarta() { 
        int index = Random.Range(0,listCards.Count);
        CardInfo cardAux = listCards[index];
        listCards.RemoveAt(index);
        return cardAux;
    }
}
