using System.Collections.Generic;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    [SerializeField] private DeckController deckController;

    [SerializeField] private GameObject handPlayer;

    [SerializeField] private List<CardInfo> inventoryCard;

    [SerializeField] private GameObject prefabCard;

    public void AñadirCartasComienzo()
    {
        for (int i = 0; i < 3; i++)
        {
            inventoryCard.Add(deckController.RepartirCarta());
            Debug.Log(inventoryCard[i]);
            CardController card = Instantiate(prefabCard,gameObject.transform).GetComponent<CardController>();
            card.AgregarHandPlayer(handPlayer.transform);
            card.AgregarCardInfo(inventoryCard[i]);
            prefabCard.transform.position = new Vector3(i-1, 0, 0);
        }
    }

}
