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
            dbConnectors[index].SetUp(
            (bool res) =>
            {
                if (res)
                {
                    connector = dbConnectors[index];
                    cards = dbConnectors[index].GetCards();
                }
            });

            yield return null;
            index++;
        }
    }
}
