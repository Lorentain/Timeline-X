using UnityEngine;

[System.Serializable]
public class PowerUpI2 : MonoBehaviour, IPowerUp
{
    public void Execute()
    {
        Debug.Log("Power Up 2 funcionando");
    }
}
