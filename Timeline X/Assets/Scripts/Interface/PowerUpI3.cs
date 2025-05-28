using UnityEngine;

public class PowerUp3 : MonoBehaviour, IPowerUp
{
    [SerializeField] private CardInventory cardInventory;
    [SerializeField] private bool isUsed = false;

    private void OnMouseDown()
    {
        if (!isUsed && cardInventory.ObtenerCartaAleatoria() != null)
        {
            Execute();
            Destroy(gameObject, 2f);
            isUsed = true;
        }
    }

    public void Execute()
    {
        Debug.Log("Power Up 3 funcionando");
        if (cardInventory.ObtenerCartaAleatoria() != null)
        {
            GameObject card = cardInventory.ObtenerCartaAleatoria();
            card.GetComponent<CardController>().ActivarCartucho();
        }
    }

    public void SetCardInventory(CardInventory aux)
    {
        cardInventory = aux;
    }
}