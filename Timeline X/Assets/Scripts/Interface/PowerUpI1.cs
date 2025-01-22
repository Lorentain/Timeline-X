using UnityEngine;

public class PowerUpI1 : MonoBehaviour, IPowerUp
{
    [SerializeField] private CardInventory cardInventory;

    [SerializeField] private bool isUsed = false;

    private void OnMouseDown()
    {
        if (!isUsed)
        {
            Execute();
            Destroy(gameObject,2f);
            isUsed = true;
        }
    }

    public void Execute()
    {
        Debug.Log("Power Up pista del año funcionando");
        GameObject card = cardInventory.ObtenerCartaAleatoria();
        card.GetComponent<CardController>().ActivarCartucho();
    }

    public void SetCardInventory(CardInventory aux)
    {
        cardInventory = aux;
    }
}
