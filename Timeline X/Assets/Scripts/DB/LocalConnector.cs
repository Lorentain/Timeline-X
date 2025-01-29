using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalConnector : DBConnector
{

    [SerializeField] private List<CardInfo> cards;

    public override List<CardInfo> GetCards()
    {
        return cards;
    }

    public override void SetUp(Action<bool> callback)
    {
        if(callback != null) {
            callback(true);
        }
    }
}
