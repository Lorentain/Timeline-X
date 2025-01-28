using UnityEngine;

public class PowerUpI2 : MonoBehaviour, IPowerUp
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
        Debug.Log("Power Up 2 funcionando");

        // Obtener una carta aleatoria
        GameObject card = cardInventory.ObtenerCartaAleatoria();
        if (card != null)
        {
            // Llamar al método para activar el cartucho de la carta con la fecha incompleta
            card.GetComponent<CardController>().ActivarCartucho(true); // 'true' para mostrar la fecha incompleta
        }
    }

    public void SetCardInventory(CardInventory aux)
    {
        cardInventory = aux;
    }
}