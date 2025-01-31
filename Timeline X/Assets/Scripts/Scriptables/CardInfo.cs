using System;
using UnityEngine;

[Serializable]
public class CardInfo
{
    [SerializeField] private string cardName;

    [SerializeField] private string cardDescription;

    [SerializeField] private string cardCategory;

    [SerializeField] private int cardDateDay;

    [SerializeField] private int cardDateMonth;

    [SerializeField] private int cardDateYear;

    [SerializeField] private Sprite cardImage;

    public string CardName
    {
        get
        {
            return cardName;
        }
        set
        {
            cardName = value;
        }
    }

    public string CardDescription
    {
        get
        {
            return cardDescription;
        }
        set
        {
            cardDescription = value;
        }
    }

    public string CardCategory
    {
        get
        {
            return cardCategory;
        }
        set
        {
            cardCategory = value;
        }
    }

    public int CardDateDay
    {
        get
        {
            return cardDateDay;
        }
        set
        {
            cardDateDay = value;
        }
    }

    public int CardDateMonth
    {
        get
        {
            return cardDateMonth;
        }
        set
        {
            cardDateMonth = value;
        }
    }

    public int CardDateYear
    {
        get
        {
            return cardDateYear;
        }
        set
        {
            cardDateYear = value;
        }
    }

    public Sprite CardImage
    {
        get
        {
            return cardImage;
        }
        set
        {
            cardImage = value;
        }
    }
}
