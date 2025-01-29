using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayFabConnector : DBConnector
{
    public override List<CardInfo> GetCards()
    {
        throw new System.NotImplementedException();
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
