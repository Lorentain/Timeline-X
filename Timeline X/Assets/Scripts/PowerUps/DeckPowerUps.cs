using System.Collections.Generic;
using UnityEngine;

public class DeckPowerUps : MonoBehaviour
{
    [SerializeField] private List<IPowerUp> iPowerUp;

    //[SerializeField] private List<PowerUp> powerUps;

    //[SerializeField] private List<PowerUpAbstract> powerupabstract;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //powerUps[0].Execute();
            //powerupabstract[0].Execute();
            iPowerUp[0].Execute();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            iPowerUp[1].Execute();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            iPowerUp[2].Execute();
        }
    }
}
