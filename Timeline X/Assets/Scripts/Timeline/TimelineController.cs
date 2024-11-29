using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    private static TimelineController instance;

    [SerializeField] private List<GameObject> cardsTimeline;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementEase;

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

        if (instance.cardsTimeline.Count != 0)
        {
            for (int i = 0; i < instance.cardsTimeline.Count; i++)
            {
                if (instance.cardsTimeline[i].transform.position.x >= 0)
                {
                    instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 1, instance.movementTime).SetEase(instance.movementEase);
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
            if (i >= index)
            {
                instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x - 1, instance.movementTime).SetEase(instance.movementEase);
            }
            else if (index == instance.cardsTimeline.Count)
            {
                instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 1, instance.movementTime).SetEase(instance.movementEase);
            }
        }
    }

    public static void MoverDerechaCartaTimeline(GameObject gameObject)
    {
        if (instance.cardsTimeline.Count != 0)
        {
            Debug.Log("Aquiii");
            if (instance.cardsTimeline.IndexOf(gameObject) != (instance.cardsTimeline.Count - 1))
            {
                Debug.Log("Entra");
                for (int i = 0; i < instance.cardsTimeline.Count; i++)
                {
                    if (i == (instance.cardsTimeline.IndexOf(gameObject) + 1))
                    {
                        instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x - 2, instance.movementTime).SetEase(instance.movementEase);
                    }
                    else if (instance.cardsTimeline[i] != gameObject)
                    {
                        instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x - 1, instance.movementTime).SetEase(instance.movementEase);
                    }
                }
                int index = instance.cardsTimeline.IndexOf(gameObject);
                instance.cardsTimeline.Insert(index + 2, gameObject);
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
                Debug.Log("Entra");
                for (int i = 0; i < instance.cardsTimeline.Count; i++)
                {
                    if (i == (instance.cardsTimeline.IndexOf(cardGameObject) - 1))
                    {
                        instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 2, instance.movementTime).SetEase(instance.movementEase);
                    }
                    else if (instance.cardsTimeline[i] != cardGameObject)
                    {
                        instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 1, instance.movementTime).SetEase(instance.movementEase);
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
