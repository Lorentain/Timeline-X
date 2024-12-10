using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CardInfo", menuName = "Timeline X/Carta")]
public class CardInfo : ScriptableObject
{
    [SerializeField] private string cardName;

    [SerializeField] private string cardDescription;

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
    }

    public string CardDescription
    {
        get
        {
            return cardDescription;
        }
    }

    public int CardDateDay
    {
        get
        {
            return cardDateDay;
        }
    }

    public int CardDateMonth
    {
        get
        {
            return cardDateMonth;
        }
    }

    public int CardDateYear
    {
        get
        {
            return cardDateYear;
        }
    }
}
