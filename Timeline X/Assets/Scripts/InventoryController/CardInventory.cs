using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    [SerializeField] private DeckController deckController;

    [SerializeField] private GameObject handPlayer;

    [SerializeField] private List<GameObject> inventoryCard;

    [SerializeField] private GameObject prefabCard;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementeEase;

    // Método para contar el número de cartas
    public int ContarCartas()
    {
        return inventoryCard.Count;
    }

    // Método que devuelve true si el inventario está vacío
    public bool EstaVacío()
    {
        return inventoryCard.Count == 0;
    }

    // Añadir cartas al inventario al inicio del juego
    public void AñadirCartasComienzo()
    {
        inventoryCard.Clear();  // Aseguramos que el inventario esté vacío al principio

        for (int i = 0; i < 3; i++)
        {
            CardInfo aux = deckController.RepartirCarta();
            Debug.Log(aux);
            CardController card = Instantiate(prefabCard, gameObject.transform).GetComponent<CardController>();
            card.AgregarCardInvetory(this);
            card.AgregarHandPlayer(handPlayer.transform);
            card.AgregarCardInfo(aux);
            inventoryCard.Add(card.gameObject);
            prefabCard.transform.position = new Vector3(i - 1, 0, 0);
        }
    }

    public void MoverCartasInventario(GameObject card)
    {
        for (int i = 0; i < inventoryCard.Count; i++)
        {
            if (i == inventoryCard.IndexOf(card) + 1)
            {
                Debug.Log("Las cartas del inventario se mueven");
                inventoryCard[i].transform.DOMoveX(inventoryCard[i].transform.position.x - 1, movementTime).SetEase(movementeEase);
            }
        }
    }
}