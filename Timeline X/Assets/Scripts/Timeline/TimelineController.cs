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

        if (cardsTimeline.Count != 0)
        {
            for (int i = 0; i < cardsTimeline.Count; i++)
            {
                cardsTimeline[i].transform.DOMoveX(cardsTimeline[i].transform.position.x + 1, movementTime).SetEase(movementEase);
            }
            cardsTimeline.Insert(0,gameObject);
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
        cardsTimeline.Remove(gameObject);
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
                    if (cardsTimeline[i].transform.position.x == (cardsTimeline.IndexOf(gameObject) + 1))
                    {
                        cardsTimeline[i].transform.DOMoveX(cardsTimeline[i].transform.position.x - 2,movementTime).SetEase(movementEase);
                    }else if(cardsTimeline[i] != gameObject) {
                        cardsTimeline[i].transform.DOMoveX(cardsTimeline[i].transform.position.x - 1,movementTime).SetEase(movementEase);
                    }
                }
            }
        }
    }
}
