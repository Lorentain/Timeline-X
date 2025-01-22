using UnityEngine;

public class PowerUpI3 : MonoBehaviour, IPowerUp
{

    private void OnMouseDown()
    {
        Execute();
    }

    public void Execute()
    {
        Debug.Log("Power Up 3 funcionando");
    }
}
