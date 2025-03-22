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

    // Variables para los paneles de información de cada jugador
    [SerializeField] private GameObject panelInfoPlayer1;

    [SerializeField] private GameObject panelInfoPlayer2;

    [SerializeField] private GameObject panelInfoPlayer3;

    [SerializeField] private GameObject panelInfoPlayer4;

    // Variables para el texto de la cantidad de power ups
    [SerializeField] private TextMeshProUGUI countPowerUpsP1;

    [SerializeField] private TextMeshProUGUI countPowerUpsP2;

    [SerializeField] private TextMeshProUGUI countPowerUpsP3;

    [SerializeField] private TextMeshProUGUI countPowerUpsP4;

    // Variables para el texto de la cantidad de cartas
    [SerializeField] private TextMeshProUGUI countCardsP1;

    [SerializeField] private TextMeshProUGUI countCardsP2;

    [SerializeField] private TextMeshProUGUI countCardsP3;

    [SerializeField] private TextMeshProUGUI countCardsP4;

    // Variables para el texto del nombre de cada jugador

    [SerializeField] private TextMeshProUGUI namePlayer1;

    [SerializeField] private TextMeshProUGUI namePlayer2;

    [SerializeField] private TextMeshProUGUI namePlayer3;

    [SerializeField] private TextMeshProUGUI namePlayer4;

    // Otras variables locales

    [SerializeField] private Camera camera;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementEase;

    [SerializeField] private GameObject specificGroupToToggle;

    [SerializeField] private GameObject feedGroupToToggle;

    [SerializeField] private bool animationDescriptionZoom = false;

    [SerializeField] private float showZoomOrthoSize;

    [SerializeField] private float normalZoomOrthoSize;

    public TMP_Text playerTurnText;
    public TMP_Text roundText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        namePlayer1.text = GameController.CambiarNombreJugadores("Jugador 1");
        namePlayer2.text = GameController.CambiarNombreJugadores("Jugador 2");
        namePlayer3.text = GameController.CambiarNombreJugadores("Jugador 3");
        namePlayer4.text = GameController.CambiarNombreJugadores("Jugador 4");

        switch (PlayerPrefs.GetInt("TotalPlayers"))
        {
            case 2:
                {
                    panelInfoPlayer1.SetActive(true);
                    panelInfoPlayer2.SetActive(true);
                    panelInfoPlayer3.SetActive(false);
                    panelInfoPlayer4.SetActive(false);
                    break;
                }
            case 3:
                {
                    panelInfoPlayer1.SetActive(true);
                    panelInfoPlayer2.SetActive(true);
                    panelInfoPlayer3.SetActive(true);
                    panelInfoPlayer4.SetActive(false);
                    break;
                }
            case 4:
                {
                    panelInfoPlayer1.SetActive(true);
                    panelInfoPlayer2.SetActive(true);
                    panelInfoPlayer3.SetActive(true);
                    panelInfoPlayer4.SetActive(true);
                    break;
                }
        }
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
        playerTurnText.text = $"Player: {GameController.CambiarNombreJugadores("Jugador " + (player + 1))}";
        roundText.text = $"Round: {round}";
    }

    public static void ShowDescription(Vector3 posicionCarta)
    {

        if (!instance.animationDescriptionZoom)
        {
            instance.animationDescriptionZoom = true;
            instance.camera.DOOrthoSize(instance.showZoomOrthoSize, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
            {
                instance.canvasDescription.SetActive(true);
            });
            instance.camera.transform.DOMove(new Vector3(posicionCarta.x, posicionCarta.y + 0.2f, -7f), instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
            {
                instance.animationDescriptionZoom = false;
                if (instance.specificGroupToToggle != null)
                {
                    instance.specificGroupToToggle.SetActive(false); // Ocultar grupo espec�fico
                }
                if (instance.feedGroupToToggle != null)
                {
                    instance.feedGroupToToggle.SetActive(false); // Ocultar grupo espec�fico
                }
            });
        }

        instance.camera.DOOrthoSize(instance.showZoomOrthoSize, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
        {
            instance.canvasDescription.SetActive(true);
            if (instance.specificGroupToToggle != null)
            {
                instance.specificGroupToToggle.SetActive(false); // Ocultar grupo espec�fico
            }
            if (instance.feedGroupToToggle != null)
            {
                instance.feedGroupToToggle.SetActive(false); // Ocultar grupo espec�fico
            }
        });
        instance.camera.transform.DOMove(new Vector3(posicionCarta.x, posicionCarta.y + 0.2f, -7f), instance.movementTime).SetEase(instance.movementEase);
    }

    public static void HideDescription()
    {
        if (!instance.animationDescriptionZoom)
            instance.animationDescriptionZoom = true;
        instance.canvasDescription.SetActive(false);

        {
            instance.camera.DOOrthoSize(instance.normalZoomOrthoSize, instance.movementTime).SetEase(instance.movementEase);
            instance.camera.transform.DOMove(new Vector3(0f, 0, -7f), instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
            {
                instance.animationDescriptionZoom = false;
                if (instance.specificGroupToToggle != null)
                {
                    instance.specificGroupToToggle.SetActive(true); // Mostrar grupo espec�fico
                }
                if (instance.feedGroupToToggle != null)
                {
                    instance.feedGroupToToggle.SetActive(true); // Mostrar grupo espec�fico
                }
            });
        }
    }

    public static bool GetCanvasDescription()
    {
        return instance.canvasDescription.activeInHierarchy;
    }

    public static void PutTextDescription(string textName, string textDescription, string textYear)
    {
        Debug.Log("Descripcion: " + textDescription);
        instance.textName.text = textName;
        instance.textDescription.text = textDescription;
        instance.textYear.text = textYear;
    }

    public static bool GetAnimationDescriptionZoom()
    {
        return instance.animationDescriptionZoom;
    }

    public static bool GetActiveDescription()
    {
        Debug.Log("Animation Description: " + GetAnimationDescriptionZoom() + "Canvas: " + GetCanvasDescription());
        bool res = false;
        if (GetAnimationDescriptionZoom() || GetCanvasDescription())
        {
            res = true;
        }
        return res;

    }

    public static void HideSpecificGroup()
    {
        if (instance.specificGroupToToggle != null)
        {
            instance.specificGroupToToggle.SetActive(false); // Ocultar el grupo espec�fico
        }
    }

    public static void HideFeedSpecificGroup()
    {
        if (instance.feedGroupToToggle != null)
        {
            instance.feedGroupToToggle.SetActive(false); // Ocultar el grupo Feed espec�fico
        }
    }

    public static void UpdatePowerUpCount(string namePlayer, int amountPowerUp)
    {
        switch (namePlayer)
        {
            case "Jugador 1":
                {
                    instance.countPowerUpsP1.text = amountPowerUp.ToString();
                    break;
                }
            case "Jugador 2":
                {
                    instance.countPowerUpsP2.text = amountPowerUp.ToString();
                    break;
                }
            case "Jugador 3":
                {
                    instance.countPowerUpsP3.text = amountPowerUp.ToString();
                    break;
                }
            case "Jugador 4":
                {
                    instance.countPowerUpsP4.text = amountPowerUp.ToString();
                    break;
                }
        }
    }

    public static void UpdateCardsCount(string namePlayer, int amountCard)
    {
        switch (namePlayer)
        {
            case "Jugador 1":
                {
                    instance.countCardsP1.text = amountCard.ToString();
                    break;
                }
            case "Jugador 2":
                {
                    instance.countCardsP2.text = amountCard.ToString();
                    break;
                }
            case "Jugador 3":
                {
                    instance.countCardsP3.text = amountCard.ToString();
                    break;
                }
            case "Jugador 4":
                {
                    instance.countCardsP4.text = amountCard.ToString();
                    break;
                }
        }
    }
}