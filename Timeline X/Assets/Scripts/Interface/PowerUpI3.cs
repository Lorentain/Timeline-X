using UnityEngine;

[System.Serializable]
public class PowerUpI3 : MonoBehaviour, IPowerUp
{
    public void Execute()
    {
        Debug.Log("Power Up 3 funcionando");
    }
}
