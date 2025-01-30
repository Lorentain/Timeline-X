using System.Collections;
using System.Collections.Generic;
using PlayFab;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    private static DBManager instance;

    [SerializeField] private List<DBConnector> dbConnectors;

    private List<CardInfo> cards;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(SetUpConnector());
    }

    public static List<CardInfo> GetCards()
    {
        return instance.cards;
    }

    private IEnumerator SetUpConnector()
    {
        DBConnector connector = null;
        int index = 0;
        while ((connector == null) && index < dbConnectors.Count)
        {
            bool ready = false;
            dbConnectors[index].SetUp(
            (bool res) =>
            {
                if (res)
                {
                    connector = dbConnectors[index];
                    dbConnectors[index].GetCards((List<CardInfo> cardsList) =>
                    {
                        cards = cardsList;
                        foreach(CardInfo card in cards) {
                            Debug.Log(card.CardName);
                            Debug.Log(card.CardDateMonth);
                            Debug.Log(card.CardDateYear);
                        }
                    });
                }
                ready = true;
            });

            float waitingTime = 0;
            do
            {
                yield return null;
                waitingTime += Time.deltaTime;
            } while (!ready && waitingTime < 10);


            index++;
        }

        if (connector != null)
        {
            Debug.Log(connector.name);
        }
    }
}
