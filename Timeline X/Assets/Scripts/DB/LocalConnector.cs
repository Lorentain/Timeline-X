using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalConnector : DBConnector
{

    [SerializeField] private List<CardInfo> cards;

    public override void GetCards(Action<List<CardInfo>> callback)
    {
        if (callback != null)
        {
            Debug.Log("Cartas obtenidas en local");
            callback(cards);
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
