using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TMP_Text playerTurnText;
    public TMP_Text roundText;

    private void OnEnable()
    {
        RoundManager.OnTurnChanged += UpdateUI;
    }

    private void OnDisable()
    {
        RoundManager.OnTurnChanged -= UpdateUI;
    }

    private void UpdateUI(int player, int round)
    {
        playerTurnText.text = $"Player: {player + 1}";
        roundText.text = $"Round: {round}";
    }
}