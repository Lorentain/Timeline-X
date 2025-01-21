using UnityEngine;

public abstract class PowerUpAbstract : MonoBehaviour
{
    private void Start() {
        Debug.Log("Power Up Abstract");
    }
    
    public abstract void Execute();
}
