using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class TimelineController : MonoBehaviour
{
    private static TimelineController instance;

    [SerializeField] private List<GameObject> cardsTimeline;

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
                    instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 1, instance.movementTime).SetEase(instance.movementEase).OnComplete(() => {
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
                instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x - 1, instance.movementTime).SetEase(instance.movementEase).OnComplete(() => {
                    instance.animationPlay = false;
                });
            }
            else if (index == instance.cardsTimeline.Count && !instance.animationPlay)
            {
                instance.animationPlay = true;
                instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 1, instance.movementTime).SetEase(instance.movementEase).OnComplete(() => {
                    instance.animationPlay = false;
                });
            }
        }
    }

    public static void MoverDerechaCartaTimeline(GameObject cardGameObject)
    {
        if (instance.cardsTimeline.Count != 0)
        {
            Debug.Log("Aquiii");
            if (instance.cardsTimeline.IndexOf(cardGameObject) != (instance.cardsTimeline.Count - 1))
            {
                cardGameObject.GetComponent<SortingGroup>().sortingOrder += 1;
                Debug.Log("Entra");
                for (int i = 0; i < instance.cardsTimeline.Count; i++)
                {
                    if (i == (instance.cardsTimeline.IndexOf(cardGameObject) + 1) && !instance.animationPlay)
                    {
                        instance.animationPlay = true;
                        instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x - 2, instance.movementTime).SetEase(instance.movementEase).OnComplete(() => {
                            instance.animationPlay = false;
                            cardGameObject.GetComponent<SortingGroup>().sortingOrder -= 1;
                        });
                    }
                    else if (instance.cardsTimeline[i] != cardGameObject && !instance.animationPlay)
                    {
                        instance.animationPlay = true;
                        instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x - 1, instance.movementTime).SetEase(instance.movementEase).OnComplete(() => {
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
        if (instance.cardsTimeline.Count != 0)
        {
            Debug.Log("Aquiii");
            if (instance.cardsTimeline.IndexOf(cardGameObject) != 0)
            {
                cardGameObject.GetComponent<SortingGroup>().sortingOrder += 1;
                Debug.Log("Entra");
                for (int i = 0; i < instance.cardsTimeline.Count; i++)
                {
                    if (i == (instance.cardsTimeline.IndexOf(cardGameObject) - 1) && !instance.animationPlay)
                    {
                        instance.animationPlay = true;
                        instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 2, instance.movementTime).SetEase(instance.movementEase).OnComplete(() => {
                            instance.animationPlay = false;
                            cardGameObject.GetComponent<SortingGroup>().sortingOrder -= 1;
                        });
                    }
                    else if (instance.cardsTimeline[i] != cardGameObject && !instance.animationPlay)
                    {
                        instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 1, instance.movementTime).SetEase(instance.movementEase).OnComplete(() => {
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

    public static Vector3 TimelinePosicion()
    {
        return instance.transform.position;
    }

    public static Transform TimelineTransform()
    {
        return instance.transform;
    }
}
