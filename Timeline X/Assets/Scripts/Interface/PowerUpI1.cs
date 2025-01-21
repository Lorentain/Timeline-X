using UnityEngine;

public class PowerUpI1 : MonoBehaviour, IPowerUp
{

    private void OnMouseDown()
    {
        Execute();
    }

    public void Execute()
    {
        Debug.Log("Power Up 1 funcionando");
    }
}
