using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private CardInventory cardInventoryPlayer1;

    [SerializeField] private CardInventory cardInventoryPlayer2;

    void Start()
    {
        cardInventoryPlayer1.AñadirCartasComienzo();
        cardInventoryPlayer2.AñadirCartasComienzo();
    }
}
