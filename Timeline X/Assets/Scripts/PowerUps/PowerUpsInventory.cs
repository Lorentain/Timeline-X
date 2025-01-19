using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PowerUpsInventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> inventoryPowerUps;

    [SerializeField] private int givePowerUpsStart;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementeEase;

    public void AñadirPowerUpsComienzo() {
        for(int i = 0; i < givePowerUpsStart; i++) {
            Debug.Log("Dado power up: " + i);
        }
    }
}
