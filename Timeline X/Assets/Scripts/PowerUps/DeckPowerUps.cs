using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeckPowerUps : MonoBehaviour
{
    [SerializeField] private List<GameObject> powerUpsInterface;

    //[SerializeField] private List<Object> powerUpsObject;

    //[SerializeField] private List<PowerUp> powerUps;

    //[SerializeField] private List<PowerUpAbstract> powerupabstract;

    // private void Start() {
    //     powerUpsInterface = new List<IPowerUp>();
    //     foreach(var powerUp in powerUpsObject) {
    //         powerUpsInterface.Add(((GameObject)powerUp).GetComponent<IPowerUp>());
    //     }
    // }

    public GameObject RepartirPowerUp() {
        int index = Random.Range(0, powerUpsInterface.Count);
        GameObject powerUp = Instantiate(powerUpsInterface[index]);
        return powerUp;
    }
    
}
