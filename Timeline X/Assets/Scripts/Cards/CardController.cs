
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CardController : MonoBehaviour
{
    [SerializeField] private CardInfo cardInfo; // Información de la carta (imagen, año, etc.)
    [SerializeField] private CardInventory player; // Referencia al inventario del jugador
    [SerializeField] private Transform handPlayer; // Transform del contenedor de cartas en la mano
    [SerializeField] private SpriteRenderer spriteRendererImagen; // Renderizador de la imagen de la carta
    [SerializeField] private GameObject buttonToDestroy; // Botón que se destruye al confirmar la carta
    [SerializeField] private TextMeshProUGUI textYear; // Texto que muestra el año en la carta
    [SerializeField] private float movementTime; // Duración del movimiento de la animación
    [SerializeField] private Ease movementEase; // Tipo de animación (facilitada por DOTween)
    [SerializeField] private bool inTimeline = false; // Indica si la carta está en la línea de tiempo
    [SerializeField] private bool animationPlay = false; // Controla si hay una animación en curso

    public void MoverCartaTimeline()
    {
        // Verifica si la carta no está ya en la línea de tiempo, si el jugador no está moviendo otra carta,
        // y si no hay una descripción activa en el UI
        if (!inTimeline && !player.ObtenerIsCardMovement() && !UIManager.GetActiveDescription())
        {
            // Intenta añadir la carta al timeline y verifica que no haya una animación activa
            if (TimelineController.AñadirCartaTimeline(gameObject) && !animationPlay)
            {
                // Incrementa el orden de sorting para que la carta se dibuje por encima de otras
                GetComponent<SortingGroup>().sortingOrder += 1;

                // Marca que una animación está en progreso
                animationPlay = true;

                // Inicia la animación que mueve la carta hacia la posición del timeline
                gameObject.transform.DOMove(TimelineController.TimelinePosicion(), movementTime)
                    .SetEase(movementEase)
                    .OnComplete(() =>
                    {
                        // Cuando la animación termine, actualiza los estados
                        animationPlay = false; // Marca que la animación ha terminado
                        GetComponent<SortingGroup>().sortingOrder -= 1; // Restaura el orden de sorting original
                    });

                // Notifica al inventario del jugador que esta carta se ha movido al timeline
                player.MoverHaciaTimeline(transform.gameObject);

                // Marca que la carta ahora está en la línea de tiempo
                inTimeline = true;

                // Imprime un mensaje en la consola para depuración
                Debug.Log("Movimiento de carta al timeline");
            }
        }
    }

    public void DevolverCartaAMano()
    {
        // Muestra un mensaje de depuración en la consola para verificar si hay una descripción activa en el UI
        Debug.Log("AQUI MIRA ANIMACION: " + UIManager.GetActiveDescription());

        // Comprueba si la carta está en la línea de tiempo, si no hay una animación en curso,
        // y si no hay una descripción activa en el UI
        if (inTimeline && !animationPlay && !UIManager.GetActiveDescription())
        {
            // Incrementa el orden de sorting para asegurar que la carta se dibuje por encima de otros objetos durante la animación
            GetComponent<SortingGroup>().sortingOrder += 1;

            // Notifica al inventario del jugador que esta carta debe regresar al inventario
            player.MoverHaciaInventario(transform.gameObject);

            // Marca que una animación está en progreso para evitar conflictos con otras animaciones
            animationPlay = true;

            // Inicia una animación para mover la carta hacia su posición inicial en el eje Y
            gameObject.transform.DOLocalMoveY(0, movementTime)
                .SetEase(movementEase) // Usa una curva de animación configurada
                .OnComplete(() =>
                {
                    // Cuando la animación termina, actualiza el estado de la carta
                    animationPlay = false; // Indica que la animación ha finalizado
                    GetComponent<SortingGroup>().sortingOrder -= 1; // Restaura el orden de sorting original
                });

            // Elimina la carta de la línea de tiempo utilizando el controlador del timeline
            TimelineController.EliminarCartaTimeline(gameObject);

            // Marca que la carta ya no está en la línea de tiempo
            inTimeline = false;
        }
    }

    public bool ConfirmarCartaTimeline()
    {
        bool res = false;

        // Verifica si la carta está en la línea de tiempo, si no hay una descripción activa en el canvas,
        // y si no hay una animación activa de zoom en la descripción
        if (inTimeline && !UIManager.GetCanvasDescription() && !UIManager.GetAnimationDescriptionZoom())
        {
            // Cambia el padre de la carta para que esté bajo el transform de la línea de tiempo
            gameObject.transform.parent = TimelineController.TimelineTransform();

            // Notifica al inventario del jugador que el movimiento de la carta ha sido confirmado
            player.ConfirmarCardMovement();

            // Actualiza el texto del año de la carta utilizando la información almacenada en CardInfo
            textYear.text = cardInfo.CardDateYear.ToString();

            // Llama al método para verificar si la carta es correcta en el timeline y hacerla parpadear
            ComprobarYParpadear();

            // Destruye el botón asociado a la carta, ya que se ha confirmado su posición en el timeline
            Destroy(buttonToDestroy);

            res = true;
        }

        return res;
    }

    // Método para comprobar y parpadear la carta
    private void ComprobarYParpadear()
    {
        bool esCorrecta = TimelineController.ComprobarCarta(gameObject);
        Color colorFinal = esCorrecta ? Color.green : Color.red;


        spriteRendererImagen.DOColor(colorFinal, 0.2f)
            .OnComplete(() =>
            {

                spriteRendererImagen.DOColor(Color.white, 0.2f);
            });
    }

    public void AgregarHandPlayer(Transform gameObject)
    {
        handPlayer = gameObject;
    }

    public void AgregarCardInfo(CardInfo aux)
    {
        cardInfo = aux;
        spriteRendererImagen.sprite = cardInfo.CardImage;
    }

    public void AgregarCardInvetory(CardInventory cardInventory)
    {
        player = cardInventory;
    }

    public void AgregarAñoCardInfo(CardInfo cardInfo)
    {
        textYear.text = cardInfo.CardDateYear.ToString();
        GetComponentInChildren<ButtonDescriptionController>().ConfirmInTimeline();
    }

    public CardInventory ObtenerInventario()
    {
        return player;
    }

    public CardInfo ObtenerCardInfo()
    {
        return cardInfo;
    }

    public Vector3 ObtenerPosicionCarta()
    {
        return gameObject.transform.position;
    }

    public bool IsTimeline()
    {
        return inTimeline;
    }

    public int ObtenerAñoCarta()
    {
        return cardInfo.CardDateYear;
    }
}