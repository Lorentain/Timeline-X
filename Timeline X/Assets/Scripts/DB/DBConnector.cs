using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DBConnector : MonoBehaviour
{
    public abstract void SetUp(Action<bool> callback);

    public abstract List<CardInfo> GetCards();
}
