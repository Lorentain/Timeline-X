using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.EconomyModels;
using UnityEngine;

public class PlayFabConnector : DBConnector
{
    public override void GetCards(Action<List<CardInfo>> callback)
    {
        Debug.Log("Cartas obtenidas en PlayFab");
        PlayFabEconomyAPI.SearchItems(new SearchItemsRequest(),
        (SearchItemsResponse response) =>
        {
            if(callback != null) {
                List<CardInfo> res = new List<CardInfo>();
                for(int i = 0; i < response.Items.Count; i++) {
                    CardInfo card = JsonUtility.FromJson<CardInfo>(response.Items[i].DisplayProperties.ToString());
                    card.CardName = response.Items[i].Title["NEUTRAL"];
                    res.Add(card);
                }

                callback(res);
            }
        },
        (PlayFabError error) =>
        {
            Debug.LogWarning("PlayFab GetCards ha fallado");
        });
    }

    public override void SetUp(Action<bool> callback)
    {
        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest { CustomId = "DEA0EF1AD3E742EB" };
        PlayFabClientAPI.LoginWithCustomID(request,
        (LoginResult result) =>
        {
            callback(true);
        },
        (PlayFabError Error) =>
        {
            callback(false);
        });
    }
}
