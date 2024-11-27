using System.Collections.Generic;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    [SerializeField] private DeckController deckController;

    [SerializeField] private List<CardInfo> inventoryCard;

    [SerializeField] private GameObject prefabCard;

    public void AñadirCartasComienzo() {
        inventoryCard.Add(deckController.RepartirCarta());
        Debug.Log(inventoryCard[0]);
        Instantiate(prefabCard);
        prefabCard.transform.position = new Vector3(0f,-3.5f,0);
    }
    
}
