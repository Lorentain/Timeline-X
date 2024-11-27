using System;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    [SerializeField] private List<GameObject> cardsTimeline;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementEase;

    private void Start()
    {
        cardsTimeline = new List<GameObject>();
    }

    public bool AñadirCartaTimeline(GameObject gameObject)
    {
        bool aux = false;
        int positionLastCard = 0;

        if (cardsTimeline.Count != 0)
        {
            for (int i = 0; i < cardsTimeline.Count; i++)
            {
                if(cardsTimeline[i].transform.position.x >= 0) {
                    cardsTimeline[i].transform.DOMoveX(cardsTimeline[i].transform.position.x + 1, movementTime).SetEase(movementEase);
                }
                if(cardsTimeline[i].transform.position.x == 0) {
                    positionLastCard = i;
                }
            }
            cardsTimeline.Insert(positionLastCard, gameObject);
            aux = true;
        }
        else
        {
            cardsTimeline.Add(gameObject);
            aux = true;
        }

        return aux;
    }

    public void EliminarCartaTimeline(GameObject gameObject)
    {
        int index = cardsTimeline.IndexOf(gameObject);
        cardsTimeline.Remove(gameObject);
        Debug.Log(index);
        for (int i = 0; i < cardsTimeline.Count; i++) {
            if(i >= index) {
                cardsTimeline[i].transform.DOMoveX(cardsTimeline[i].transform.position.x -1, movementTime).SetEase(movementEase);
            }else if(index == cardsTimeline.Count) {
                cardsTimeline[i].transform.DOMoveX(cardsTimeline[i].transform.position.x +1, movementTime).SetEase(movementEase);
            }
        }
    }

    public void MoverDerechaCartaTimeline(GameObject gameObject)
    {
        if (cardsTimeline.Count != 0)
        {
            Debug.Log("Aquiii");
            if (cardsTimeline.IndexOf(gameObject) != (cardsTimeline.Count - 1))
            {
                Debug.Log("Entra");
                for (int i = 0; i < cardsTimeline.Count; i++)
                {
                    if (i == (cardsTimeline.IndexOf(gameObject) + 1))
                    {
                        cardsTimeline[i].transform.DOMoveX(cardsTimeline[i].transform.position.x - 2, movementTime).SetEase(movementEase);
                    }
                    else if (cardsTimeline[i] != gameObject)
                    {
                        cardsTimeline[i].transform.DOMoveX(cardsTimeline[i].transform.position.x - 1, movementTime).SetEase(movementEase);
                    }
                }
                int index = cardsTimeline.IndexOf(gameObject);
                cardsTimeline.Insert(index + 2, gameObject);
                cardsTimeline.RemoveAt(index);
            }
        }
    }

    public void MoverIzquierdaCartaTimeline(GameObject cardGameObject)
    {
        if (cardsTimeline.Count != 0)
        {
            Debug.Log("Aquiii");
            if (cardsTimeline.IndexOf(cardGameObject) != 0)
            {
                Debug.Log("Entra");
                for (int i = 0; i < cardsTimeline.Count; i++)
                {
                    if (i == (cardsTimeline.IndexOf(cardGameObject) - 1))
                    {
                        cardsTimeline[i].transform.DOMoveX(cardsTimeline[i].transform.position.x + 2, movementTime).SetEase(movementEase);
                    }
                    else if (cardsTimeline[i] != cardGameObject)
                    {
                        cardsTimeline[i].transform.DOMoveX(cardsTimeline[i].transform.position.x + 1, movementTime).SetEase(movementEase);
                    }
                }
                int index = cardsTimeline.IndexOf(cardGameObject);
                cardsTimeline.Insert(index - 1, cardGameObject);
                cardsTimeline.RemoveAt(index + 1);
            }
        }
    }
}
