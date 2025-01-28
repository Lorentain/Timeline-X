using UnityEngine;

public class PowerUp3 : MonoBehaviour, IPowerUp
{
    [SerializeField] private CardInventory cardInventory;
    [SerializeField] private bool isUsed = false;

    private void OnMouseDown()
    {
        if (!isUsed)
        {
            Execute();
            Destroy(gameObject, 2f);
            isUsed = true;
        }
    }

    public void Execute()
    {
        Debug.Log("Power Up 3 funcionando");

        // Obtener una carta aleatoria
        GameObject card = cardInventory.ObtenerCartaAleatoria();
        if (card != null)
        {
            // Llamar al método ActivarCartucho con el PowerUp 3 activado
            card.GetComponent<CardController>().ActivarCartucho(isPowerUp3Active: true);
        }
    }

    public void SetCardInventory(CardInventory aux)
    {
        cardInventory = aux;
    }
}