using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalConnector : DBConnector
{

    [SerializeField] private List<CardData> cards;

    public override void GetCards(Action<List<CardInfo>> callback)
    {
        if (callback != null)
        {
            Debug.Log("Cartas obtenidas en local");
            List<CardInfo> list = new List<CardInfo>();
            foreach(CardData card in cards) {
                list.Add(card.cardInfo);
            }
            callback(list);
        }
    }

    public override void SetUp(Action<bool> callback)
    {
        if (callback != null)
        {
            callback(true);
        }
    }
}
