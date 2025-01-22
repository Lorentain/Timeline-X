using UnityEngine;

public class PowerUpI2 : MonoBehaviour, IPowerUp
{

    private void OnMouseDown()
    {
        Execute();
    }
    public void Execute()
    {
        Debug.Log("Power Up 2 funcionando");
    }

    public void SetCardInventory(CardInventory aux)
    {
        
    }
}
