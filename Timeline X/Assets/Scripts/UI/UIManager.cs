using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject canvasDescription; // El canvas que contiene la descripción
    [SerializeField] private GameObject specificGroupToToggle; // El grupo específico que se activa/desactiva
    [SerializeField] private Camera mainCamera; // Cámara principal
    [SerializeField] private float movementTime = 0.5f; // Tiempo de animación de movimiento
    [SerializeField] private Ease movementEase = Ease.OutExpo; // Tipo de animación

    public TMP_Text playerTurnText;
    public TMP_Text roundText;

    private TextMeshProUGUI textDescription;

    private void Awake()
    {
        instance = this;
        textDescription = canvasDescription.GetComponentInChildren<TextMeshProUGUI>(); // Obtener referencia al TextMeshProUGUI
    }

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

    public static void ShowDescription(Vector3 posicionCarta)
    {
        instance.mainCamera.DOOrthoSize(0.5f, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
        {
            instance.canvasDescription.SetActive(true);
            if (instance.specificGroupToToggle != null)
            {
                instance.specificGroupToToggle.SetActive(false); // Ocultar grupo específico
            }
        });

        instance.mainCamera.transform.DOMove(new Vector3(posicionCarta.x, posicionCarta.y, -10f), instance.movementTime).SetEase(instance.movementEase);
    }

    public static void HideDescription()
    {
        instance.canvasDescription.SetActive(false);
        instance.mainCamera.DOOrthoSize(4.5f, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
        {
            if (instance.specificGroupToToggle != null)
            {
                instance.specificGroupToToggle.SetActive(true); // Mostrar grupo específico
            }
        });

        instance.mainCamera.transform.DOMove(new Vector3(0f, 0, -10f), instance.movementTime).SetEase(instance.movementEase);
    }

    public static bool GetCanvasDescription()
    {
        return instance.canvasDescription.activeInHierarchy;
    }

    public static void PutTextDescription(string textDescription)
    {
        if (instance.textDescription != null)
        {
            instance.textDescription.text = textDescription; // Asignar el texto al TextMeshProUGUI
        }
    }

    public static void HideSpecificGroup()
    {
        if (instance.specificGroupToToggle != null)
        {
            instance.specificGroupToToggle.SetActive(false); // Ocultar el grupo específico
        }
    }
}