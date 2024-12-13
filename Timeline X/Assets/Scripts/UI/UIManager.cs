using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;


    [SerializeField] private GameObject canvasDescription;

    [SerializeField] private TextMeshProUGUI textName;

    [SerializeField] private TextMeshProUGUI textDescription;

    [SerializeField] private TextMeshProUGUI textYear;

    [SerializeField] private Camera camera;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementEase;

    [SerializeField] private GameObject specificGroupToToggle; 

    [SerializeField] private GameObject feedGroupToToggle; 
    
    
     


    [SerializeField] private bool animationDescriptionZoom = false;

    public TMP_Text playerTurnText;
    public TMP_Text roundText;

    

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

        instance.camera.DOOrthoSize(0.5f, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
        {
            instance.canvasDescription.SetActive(true);
            if (instance.specificGroupToToggle != null)
            {
                instance.specificGroupToToggle.SetActive(false); // Ocultar grupo específico
            }
            if (instance.feedGroupToToggle != null)
            {
                instance.feedGroupToToggle.SetActive(false); // Ocultar grupo específico
            }
        });

        instance.camera.transform.DOMove(new Vector3(posicionCarta.x, posicionCarta.y, -10f), instance.movementTime).SetEase(instance.movementEase);
 
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

        instance.camera.DOOrthoSize(4.5f, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
        {
            if (instance.specificGroupToToggle != null)
            {
                instance.specificGroupToToggle.SetActive(true); // Mostrar grupo específico
            }
            if (instance.feedGroupToToggle != null)
            {
                instance.feedGroupToToggle.SetActive(true); // Mostrar grupo específico
            }
        });

        instance.camera.transform.DOMove(new Vector3(0f, 0, -10f), instance.movementTime).SetEase(instance.movementEase);

    }

    public static bool GetCanvasDescription()
    {
        return instance.canvasDescription.activeInHierarchy;
    }

    public static void PutTextDescription(string textName,string textDescription, int textYear)
    {

        instance.textName.text = textName;
        instance.textDescription.text = textDescription;
        instance.textYear.text = textYear.ToString();
    }

    public static bool GetAnimationDescriptionZoom() {
        return instance.animationDescriptionZoom;
    }

    public static bool GetActionDescription() {
        bool res = false;
        if(!GetAnimationDescriptionZoom() && !GetCanvasDescription()) {
            res = true;
        }
        return res;

    }

    public static void HideSpecificGroup()
    {
        if (instance.specificGroupToToggle != null)
        {
            instance.specificGroupToToggle.SetActive(false); // Ocultar el grupo específico
        }
    }

    public static void HideFeedSpecificGroup()
    {
        if (instance.feedGroupToToggle != null)
        {
            instance.feedGroupToToggle.SetActive(false); // Ocultar el grupo Feed específico
        }
    }


}