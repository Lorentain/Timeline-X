using UnityEngine;
using TMPro;

public class VictorySceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text victoryText;  // Referencia al TextMeshPro en la escena

    void Start()
    {
        // Verifica el jugador ganador y muestra el mensaje correspondiente
        if (GameController.jugadorGanador == 1)
        {
            victoryText.text = "¡El Jugador 1 ha ganado!";
        }
        else if (GameController.jugadorGanador == 2)
        {
            victoryText.text = "¡El Jugador 2 ha ganado!";
        }
    }
}