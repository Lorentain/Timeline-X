using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PowerUpsInventory : MonoBehaviour
{
    [SerializeField] private List<IPowerUp> inventoryPowerUps;

    [SerializeField] private DeckPowerUps deckPowerUps;

    [SerializeField] private GameObject player;

    [SerializeField] private int givePowerUpsStart;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementeEase;

    private void Update()
    {

    }

    public void AñadirPowerUpsComienzo()
    {
        inventoryPowerUps = new List<IPowerUp>();
        for (int i = 0; i < givePowerUpsStart; i++)
        {
            Debug.Log("Se ha repartido un power up");
            GameObject aux = deckPowerUps.RepartirPowerUp();
            inventoryPowerUps.Add(aux.GetComponent<IPowerUp>());
            aux.transform.DOMove(new Vector3(7, -3.5f, 0), movementTime).SetEase(movementeEase);
            aux.transform.SetParent(player.transform);
            aux.SetActive(true);
            UIManager.UpdatePowerUpCount(player.name, inventoryPowerUps.Count);
        }
    }
}
