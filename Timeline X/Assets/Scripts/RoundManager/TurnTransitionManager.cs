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

    private void Start() {
        transitionText.text = $"Ahora le toca a {GameController.CambiarNombreJugadores("Jugador 1")}";
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
            // Mostrar la transición en pantalla con un retraso antes del fade in
            StartCoroutine(DelayedFadeInPanel(2f)); // Retraso de 1 segundo antes de que aparezca el panel

            transitionText.text = $"¡Turno terminado! Ahora le toca a {GameController.CambiarNombreJugadores("Jugador " + (currentPlayer + 1))}";

            continueButton.interactable = false;
            Invoke(nameof(EnableContinueButton), 2f); // Ajusta el tiempo si necesitas más sincronización
        }
        else
        {
            return;
        }
    }

    // Nueva corrutina para añadir un retraso antes del fade in
    private IEnumerator DelayedFadeInPanel(float delay)
    {
        yield return new WaitForSeconds(delay); // Esperar el retraso
        yield return FadeInPanel(); // Iniciar el fade in del panel
    }

    private void EnableContinueButton()
    {
        continueButton.interactable = true;
    }

    public void OnContinueButtonPressed()
    {
        // Comenzar la transici�n para el siguiente turno
        StartCoroutine(FadeOutPanel());
        hasFirstTurnCompleted = true;
        RoundManager.ChangePlayer();
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