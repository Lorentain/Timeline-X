using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] private GameObject canvasDescription;

    [SerializeField] private TextMeshProUGUI textDescription;

    [SerializeField] private Camera camera;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementEase;

    [SerializeField] private bool animationDescriptionZoom = false;

    public TMP_Text playerTurnText;
    public TMP_Text roundText;

    private void Awake()
    {
        instance = this;
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
        if(!instance.animationDescriptionZoom) {
            instance.animationDescriptionZoom = true;
            instance.camera.DOOrthoSize(0.5f, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
            {
                instance.canvasDescription.SetActive(true);
            });
            instance.camera.transform.DOMove(new Vector3(posicionCarta.x, posicionCarta.y, -10f), instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
            {
                instance.animationDescriptionZoom = false;
            });
        }
    }

    public static void HideDescription()
    {
        if (!instance.animationDescriptionZoom)
        instance.animationDescriptionZoom = true;
        instance.canvasDescription.SetActive(false);
        {
            instance.camera.DOOrthoSize(4.5f, instance.movementTime).SetEase(instance.movementEase);
            instance.camera.transform.DOMove(new Vector3(0f, 0, -10f), instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
            {
                instance.animationDescriptionZoom = false;
            });
        }
    }

    public static bool GetCanvasDescription()
    {
        bool res = false;
        if (instance.canvasDescription.activeInHierarchy)
        {
            res = true;
        }
        return res;
    }

    public static void PutTextDescription(string textDescription)
    {
        instance.textDescription.text = textDescription;
    }
}