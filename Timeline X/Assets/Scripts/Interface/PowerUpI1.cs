using UnityEngine;

[System.Serializable]
public class PowerUpI1 : MonoBehaviour, IPowerUp
{
    public void Execute()
    {
        Debug.Log("Power Up 1 funcionando");
    }
}
