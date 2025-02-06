using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    [SerializeField] private DeckController deckController;

    [SerializeField] private GameObject handPlayer;

    [SerializeField] private GameObject player;

    [SerializeField] private List<GameObject> inventoryCard;

    [SerializeField] private GameObject prefabCardEmpresa;

    [SerializeField] private GameObject prefabCardVideojuego;

    [SerializeField] private GameObject prefabCardEvento;

    [SerializeField] private GameObject prefabCardConsola;

    [SerializeField] private GameObject prefabCardSoftware;

    [SerializeField] private int giveCardStart;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementeEase;

    [SerializeField] private bool isCardMovement = false;

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
        for (int i = 0; i < giveCardStart; i++)
        {
            CardInfo aux = deckController.RepartirCarta();
            Debug.Log(aux);
            CardController card = null;
            switch (aux.CardCategory)
            {
                case "Empresa":
                    {
                        card = Instantiate(prefabCardEmpresa, gameObject.transform).GetComponent<CardController>();
                        break;
                    }
                case "Videojuego":
                    {
                        card = Instantiate(prefabCardVideojuego, gameObject.transform).GetComponent<CardController>();
                        break;
                    }
                case "Evento":
                    {
                        card = Instantiate(prefabCardEvento, gameObject.transform).GetComponent<CardController>();
                        break;
                    }
                case "Consola":
                    {
                        card = Instantiate(prefabCardConsola, gameObject.transform).GetComponent<CardController>();
                        break;
                    }
                case "Software":
                    {
                        card = Instantiate(prefabCardSoftware, gameObject.transform).GetComponent<CardController>();
                        break;
                    }
            }
            card.AgregarCardInventory(this);
            card.AgregarHandPlayer(handPlayer.transform);
            card.AgregarCardInfo(aux);
            inventoryCard.Add(card.gameObject);
            card.transform.localPosition = new Vector3(i - 1, 0, 0);
            ReordenarInventario();
            UIManager.UpdateCardsCount(player.name, inventoryCard.Count);
        }
    }

    public void RobarCarta()
    {
        CardInfo aux = deckController.RepartirCarta();
        Debug.Log(aux);
        CardController card = null;
        switch (aux.CardCategory)
        {
            case "Empresa":
                {
                    card = Instantiate(prefabCardEmpresa, gameObject.transform).GetComponent<CardController>();
                    break;
                }
            case "Videojuego":
                {
                    card = Instantiate(prefabCardVideojuego, gameObject.transform).GetComponent<CardController>();
                    break;
                }
            case "Evento":
                {
                    card = Instantiate(prefabCardEvento, gameObject.transform).GetComponent<CardController>();
                    break;
                }
            case "Consola":
                {
                    card = Instantiate(prefabCardConsola, gameObject.transform).GetComponent<CardController>();
                    break;
                }
            case "Software":
                {
                    card = Instantiate(prefabCardSoftware, gameObject.transform).GetComponent<CardController>();
                    break;
                }
        }
        card.AgregarCardInventory(this);
        card.AgregarHandPlayer(handPlayer.transform);
        card.AgregarCardInfo(aux);
        inventoryCard.Add(card.gameObject);
        card.transform.localPosition = new Vector3(inventoryCard.Count - 1, 0, 0);
        ReordenarInventario();
        Debug.Log("Carta robada:" + aux.CardName);
        UIManager.UpdateCardsCount(player.name, inventoryCard.Count);
    }

    public void MoverHaciaTimeline(GameObject card)
    {
        if (!isCardMovement)
        {
            inventoryCard.Remove(card);
            card.transform.parent = TimelineController.TimelineTransform();
            ReordenarInventario();
            isCardMovement = true;
        }
    }

    public void MoverHaciaInventario(GameObject card)
    {
        inventoryCard.Insert(inventoryCard.Count / 2, card);
        card.transform.parent = this.transform;
        ReordenarInventario();
        isCardMovement = false;
    }

    public void ReordenarInventario() // Formula i - (n/2 - 0.5)
    {
        float formula = (inventoryCard.Count / 2f) - 0.5f;
        for (int i = 0; i < inventoryCard.Count; i++)
        {
            inventoryCard[i].transform.DOLocalMoveX((i * 1.5f) - formula, movementTime).SetEase(movementeEase);
        }
    }

    public bool ObtenerIsCardMovement()
    {
        return isCardMovement;
    }

    public void ConfirmarCardMovement()
    {
        isCardMovement = false;
    }

    public GameObject ObtenerCartaAleatoria()
    {
        int index = Random.Range(0, ContarCartas());
        return inventoryCard[index];
    }
}
