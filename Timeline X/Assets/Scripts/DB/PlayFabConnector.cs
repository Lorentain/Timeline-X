using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.EconomyModels;
using UnityEngine;
using UnityEngine.Networking;

public class PlayFabConnector : DBConnector
{
    public override void GetCards(Action<List<CardInfo>> callback)
    {
        Debug.Log("Cartas obtenidas en PlayFab");
        PlayFabEconomyAPI.SearchItems(new SearchItemsRequest{Count = 50},
        (SearchItemsResponse response) =>
        {
            if (callback != null)
            {
                StartCoroutine(GetCardByCard(response.Items,callback));
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
            Debug.Log("No te logeas bien");
            callback(false);
        });
    }

    private IEnumerator GetCardByCard(List<PlayFab.EconomyModels.CatalogItem> items, Action<List<CardInfo>> callback) {
        List<CardInfo> res = new List<CardInfo>();
                for (int i = 0; i < items.Count; i++)
                {
                    CardInfo card = JsonUtility.FromJson<CardInfo>(items[i].DisplayProperties.ToString());
                    card.CardName = items[i].Title["NEUTRAL"];
                    yield return GetImage(items[i].Images[0].Url,
                    (Sprite image) =>
                    {
                        card.CardImage = image;
                        res.Add(card);
                    });
                }

                callback(res);
    }

    private IEnumerator GetImage(string url, Action<Sprite> callback)
    {
        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(url);
        yield return imageRequest.SendWebRequest();

        if (callback != null)
        {
            if (imageRequest.result.Equals(UnityWebRequest.Result.Success))
            {
               Texture2D texture = DownloadHandlerTexture.GetContent(imageRequest);
               texture.filterMode = FilterMode.Point;
               Sprite sprite = Sprite.Create(texture,new Rect(0, 0, texture.width,texture.height),new Vector2(0.5f,0.5f),60);
               callback(sprite);
            }
        }
    }
}
