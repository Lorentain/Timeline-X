using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TurnTransitionManager : MonoBehaviour
{
    [SerializeField] private GameObject transitionPanel;
    [SerializeField] private TMP_Text transitionText;
    [SerializeField] private Button continueButton;
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private ActionFeedManager actionFeedManager;  // Referencia al ActionFeedManager
    [SerializeField] private float fadeDuration = 1f;
    private CanvasGroup canvasGroup;

    private bool hasFirstTurnCompleted = false;

    private void Awake()
    {
        canvasGroup = transitionPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = transitionPanel.AddComponent<CanvasGroup>();
        }
    }

    private void OnEnable()
    {
        RoundManager.OnTurnChanged += HandleTurnChanged;
    }

    private void OnDisable()
    {
        RoundManager.OnTurnChanged -= HandleTurnChanged;
    }

    private void HandleTurnChanged(int currentPlayer, int currentRound)
    {
        if (hasFirstTurnCompleted || currentPlayer > 0 || currentRound > 1)
        {
            // Registrar en el feed que el turno ha terminado y que el siguiente jugador va a jugar
            actionFeedManager.LogAction($"Turno finalizado. Ahora es el turno del Jugador {currentPlayer + 1}.");

            // Mostrar la transición en pantalla
            StartCoroutine(FadeInPanel());

            transitionText.text = $"¡Turno terminado! Ahora le toca al Jugador {currentPlayer + 1}";

            continueButton.interactable = false;
            Invoke(nameof(EnableContinueButton), 1f);
        }
        else
        {
            return;
        }
    }

    private void EnableContinueButton()
    {
        continueButton.interactable = true;
    }

    public void OnContinueButtonPressed()
    {
        // Registrar en el feed cuando el jugador presione el botón de continuar
        actionFeedManager.LogAction("El jugador presionó 'Continuar', comenzando el siguiente turno.");

        // Comenzar la transición para el siguiente turno
        StartCoroutine(FadeOutPanel());
        hasFirstTurnCompleted = true;
    }

    private IEnumerator FadeInPanel()
    {
        float timeElapsed = 0f;

        canvasGroup.alpha = 0f;
        transitionPanel.SetActive(true);

        // Animar el fade in
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOutPanel()
    {
        float timeElapsed = 0f;

        // Animar el fade out
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);
            yield return null;
        }

        // Asegurar que el alpha llegue a 0 al final
        canvasGroup.alpha = 0f;
        transitionPanel.SetActive(false); // Desactivar el panel una vez que el fade haya terminado
    }
}