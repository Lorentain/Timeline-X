using UnityEngine;
using TMPro;

public class VictorySceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text victoryText;  // Referencia al TextMeshPro en la escena

    void Start()
    {
        if(GameController.jugadorGanador != -1) {
            victoryText.text = $"¡El Jugador {GameController.jugadorGanador} ha ganado!";
        }else {
            victoryText.text = "¡La partida ha quedado en empate";
        }
        
    }
}