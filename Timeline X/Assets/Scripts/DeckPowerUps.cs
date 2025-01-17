using System.Collections.Generic;
using UnityEngine;

public class DeckPowerUps : MonoBehaviour
{
    [SerializeField] private List<PowerUp> powerUps;
    [SerializeField] private List<IPowerUp> ipowerup;
    
    //[SerializeField] private List<PowerUpAbstract> powerupabstract;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            powerUps[0].Execute();
            //powerupabstract[0].Execute();
            ipowerup[0].Execute();
            powerUps.RemoveAt(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            powerUps[1].Execute();
            powerUps.RemoveAt(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            powerUps[2].Execute();
            powerUps.RemoveAt(2);
        }
    }
}
