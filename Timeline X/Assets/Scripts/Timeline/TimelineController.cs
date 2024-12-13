using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class TimelineController : MonoBehaviour
{
    private static TimelineController instance;

    [SerializeField] private List<GameObject> cardsTimeline;

    [SerializeField] private DeckController deckController;

    [SerializeField] private GameObject prefabCard;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementEase;

    [SerializeField] private bool animationPlay = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cardsTimeline = new List<GameObject>();
    }

    public static void PonerCartaInicial() {
        CardInfo aux = instance.deckController.RepartirCarta();
        CardController card = Instantiate(instance.prefabCard, instance.gameObject.transform).GetComponent<CardController>();
        card.AgregarCardInfo(aux);
        card.transform.localPosition = new Vector3(0, 0, 0);
        AñadirCartaTimeline(card.transform.gameObject);
        Destroy(card.gameObject.transform.Find("Button Destroy").gameObject);
    }

    public static bool AñadirCartaTimeline(GameObject gameObject)
    {
        bool aux = false;
        int positionLastCard = 0;

        if (instance.cardsTimeline.Count != 0 && !instance.animationPlay)
        {
            for (int i = 0; i < instance.cardsTimeline.Count; i++)
            {
                if (instance.cardsTimeline[i].transform.position.x >= 0)
                {
                    instance.animationPlay = true;
                    instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 1, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
                    {
                        instance.animationPlay = false;
                    });
                }
                if (instance.cardsTimeline[i].transform.position.x == 0)
                {
                    positionLastCard = i;
                }
            }
            instance.cardsTimeline.Insert(positionLastCard, gameObject);
            aux = true;
        }
        else
        {
            instance.cardsTimeline.Add(gameObject);
            Debug.Log("EYYY");
            aux = true;
        }

        return aux;
    }

    public static void EliminarCartaTimeline(GameObject gameObject)
    {
        int index = instance.cardsTimeline.IndexOf(gameObject);
        instance.cardsTimeline.Remove(gameObject);
        Debug.Log(index);
        for (int i = 0; i < instance.cardsTimeline.Count; i++)
        {
            if (i >= index && !instance.animationPlay)
            {
                instance.animationPlay = true;
                instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x - 1, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
                {
                    instance.animationPlay = false;
                });
            }
            else if (index == instance.cardsTimeline.Count && !instance.animationPlay)
            {
                instance.animationPlay = true;
                instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 1, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
                {
                    instance.animationPlay = false;
                });
            }
        }
    }

    public static void MoverDerechaCartaTimeline(GameObject cardGameObject)
    {
        if (instance.cardsTimeline.Count != 0 && !instance.animationPlay)
        {
            Debug.Log("Aquiii");
            if (instance.cardsTimeline.IndexOf(cardGameObject) != (instance.cardsTimeline.Count - 1))
            {
                instance.animationPlay = true;
                for (int i = 0; i < instance.cardsTimeline.Count; i++)
                {
                    if (i == (instance.cardsTimeline.IndexOf(cardGameObject) + 1))
                    {
                        cardGameObject.GetComponent<SortingGroup>().sortingOrder += 1;
                        instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x - 2, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
                        {
                            instance.animationPlay = false;
                            cardGameObject.GetComponent<SortingGroup>().sortingOrder -= 1;
                        });
                    }
                    else if (instance.cardsTimeline[i] != cardGameObject)
                    {
                        cardGameObject.GetComponent<SortingGroup>().sortingOrder += 1;
                        instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x - 1, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
                        {
                            instance.animationPlay = false;
                            cardGameObject.GetComponent<SortingGroup>().sortingOrder -= 1;
                        });
                    }
                }
                int index = instance.cardsTimeline.IndexOf(cardGameObject);
                instance.cardsTimeline.Insert(index + 2, cardGameObject);
                instance.cardsTimeline.RemoveAt(index);
            }
        }
    }

    public static void MoverIzquierdaCartaTimeline(GameObject cardGameObject)
    {
        if (instance.cardsTimeline.Count != 0 && !instance.animationPlay)
        {
            Debug.Log("Aquiii");
            if (instance.cardsTimeline.IndexOf(cardGameObject) != 0)
            {
                instance.animationPlay = true;
                for (int i = 0; i < instance.cardsTimeline.Count; i++)
                {
                    if (i == (instance.cardsTimeline.IndexOf(cardGameObject) - 1))
                    {
                        cardGameObject.GetComponent<SortingGroup>().sortingOrder += 1;
                        instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 2, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
                        {
                            instance.animationPlay = false;
                            cardGameObject.GetComponent<SortingGroup>().sortingOrder -= 1;
                        });
                    }
                    else if (instance.cardsTimeline[i] != cardGameObject)
                    {
                        cardGameObject.GetComponent<SortingGroup>().sortingOrder += 1;
                        instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 1, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
                        {
                            instance.animationPlay = false;
                            cardGameObject.GetComponent<SortingGroup>().sortingOrder -= 1;
                        });
                    }
                }
                int index = instance.cardsTimeline.IndexOf(cardGameObject);
                instance.cardsTimeline.Insert(index - 1, cardGameObject);
                instance.cardsTimeline.RemoveAt(index + 1);
            }
        }
    }

    public static bool ComprobarCarta(GameObject card)
    {
        bool res = false;

        int index = instance.cardsTimeline.IndexOf(card);

        // La carta de delante es menor
        if (index != instance.cardsTimeline.Count-1 && instance.cardsTimeline[index].gameObject.GetComponent<CardController>().ObtenerAñoCarta() > instance.cardsTimeline[index + 1].gameObject.GetComponent<CardController>().ObtenerAñoCarta())
        {
            Debug.Log("MAL DERECHA");
            Debug.Log("Mi:" + instance.cardsTimeline[index].gameObject.GetComponent<CardController>().ObtenerAñoCarta());
            Debug.Log("Derecha:" + instance.cardsTimeline[index+1].gameObject.GetComponent<CardController>().ObtenerAñoCarta());
        }
        // La carta de detras es mayor
        else if (index != 0 && instance.cardsTimeline[index].gameObject.GetComponent<CardController>().ObtenerAñoCarta() < instance.cardsTimeline[index - 1].gameObject.GetComponent<CardController>().ObtenerAñoCarta())
        {
            Debug.Log("MAL IZQUIERDA");
            Debug.Log("Mi:" + instance.cardsTimeline[index].gameObject.GetComponent<CardController>().ObtenerAñoCarta());
            Debug.Log("Izquierda:" + instance.cardsTimeline[index-1].gameObject.GetComponent<CardController>().ObtenerAñoCarta());
        }
        // La carta de delante es mayor y la de atrás es menor
        else
        {
            Debug.Log("BIEN");
        }

        return res;
    }

    public static Vector3 TimelinePosicion()
    {
        return instance.transform.position;
    }

    public static Transform TimelineTransform()
    {
        return instance.transform;
    }
}
