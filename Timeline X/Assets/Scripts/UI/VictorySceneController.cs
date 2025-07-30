using UnityEngine;
using TMPro;

public class VictorySceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text victoryText;  // Referencia al TextMeshPro en la escena

    void Start()
    {
        if(GameController.jugadorGanador != -1) {

            victoryText.text = $"The  player  {GameController.GetNameOfPlayer(GameController.jugadorGanador)}  wins";
        }else {
            victoryText.text = "The game ended in a draw";
        }
        
    }
}