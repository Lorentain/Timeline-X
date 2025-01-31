
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
    [SerializeField] private GameObject cartucho; // Referencia al GameObject del cartucho
    

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

        if (inTimeline && !UIManager.GetCanvasDescription() && !UIManager.GetAnimationDescriptionZoom())
        {
            gameObject.transform.parent = TimelineController.TimelineTransform();
            player.ConfirmarCardMovement();
            textYear.text = cardInfo.CardDateYear.ToString();

            if (cartucho != null)
            {
                ActivarCartucho();
            }
            else
            {
                Debug.LogError("El cartucho no está asignado en el Inspector");
            }

            ComprobarYParpadear();
            UIManager.UpdateCardsCount(player.name,player.ContarCartas());
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

        if (esCorrecta)
        {
            // Si la carta es correcta, hacemos una animación vistosa con parpadeo verde
            Sequence animacionCorrecta = DOTween.Sequence();

            // Repetir el parpadeo verde varias veces
            int vecesParpadeo = 3; // Número de veces que la carta parpadeará
            float duracionParpadeo = 0.1f; // Duración de cada parpadeo (verde a blanco)

            for (int i = 0; i < vecesParpadeo; i++)
            {
                // Parpadeo de verde a blanco
                animacionCorrecta.Append(spriteRendererImagen.DOColor(Color.green, duracionParpadeo));
                animacionCorrecta.Append(spriteRendererImagen.DOColor(Color.white, duracionParpadeo));
            }

            // Desplazamiento sutil (mueve un poquito la carta sin escalar)
            animacionCorrecta.Join(transform.DOMoveY(transform.position.y + 0.5f, 0.1f).From(true)); // Desplazamiento hacia arriba sutil

            // Restauración rápida de la posición (vuelve a su lugar original)
            animacionCorrecta.Append(transform.DOLocalMove(Vector3.zero, 0.2f));

            animacionCorrecta.Play();
        }
        else
        {
            // Si es incorrecta, hacemos una vibración fuerte pero en pequeña distancia
            Sequence animacionIncorrecta = DOTween.Sequence();

            // Cambio de color a rojo
            animacionIncorrecta.Append(spriteRendererImagen.DOColor(colorFinal, 0.3f));

            // Vibración fuerte pero en una distancia pequeña (aumentando el desplazamiento)
            animacionIncorrecta.Join(transform.DOShakePosition(0.1f, 0.5f, 100, 5, false, true)); // Vibración intensa pero pequeña

            // Restaurar color y posición original
            animacionIncorrecta.Append(spriteRendererImagen.DOColor(Color.white, 0.1f));
            
            animacionIncorrecta.Play();
        }
    }

    public void AgregarHandPlayer(Transform gameObject)
    {
        handPlayer = gameObject;
    }

    public void AgregarCardInfo(CardInfo aux)
    {
        cardInfo = aux;
        Debug.Log(cardInfo);
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

    public void ActivarCartucho(bool isPowerUp2Active = false, bool isPowerUp3Active = false)
    {
        // Activa el cartucho para que sea visible
        cartucho.SetActive(true);

        // Guarda la posición final del cartucho
        Vector3 finalPosition = cartucho.transform.localPosition;

        // Coloca el cartucho unos 100 unidades más abajo en el eje Y
        cartucho.transform.localPosition = finalPosition + new Vector3(0, -1f, 0);

        // Anima el cartucho hacia su posición final
        cartucho.transform.DOLocalMove(finalPosition, 1f).SetEase(Ease.OutCubic).OnComplete(() => {
            string yearString = cardInfo.CardDateYear.ToString();

            // Si PowerUp 2 está activo, mostramos la fecha incompleta (XXX_X)
            if (isPowerUp2Active)
            {
                string incompleteYear = yearString.Substring(0, 3) + "X"; // Año incompleto
                textYear.text = incompleteYear;
            }
            // Si PowerUp 3 está activo, mostramos el abanico de años con el año real incluido
            else if (isPowerUp3Active)
            {
                // Obtener el año real de la carta
                int year = cardInfo.CardDateYear;

                // Definir un rango de ±5 años (rango total de 10 años)
                int range = 5;

                // Generar un desplazamiento aleatorio dentro de los ±5 años
                int offset = Random.Range(-range, range + 1);

                // Calcular el rango real de años
                int startRange = Mathf.Max(year + offset - range, 0); // Asegura que no sea menor que 0
                int endRange = year + offset + range;

                // Generar dos opciones aleatorias dentro del rango que incluya el año real
                int firstOption = Random.Range(startRange, endRange + 1);
                int secondOption = Random.Range(startRange, endRange + 1);

                // Asegurarse de que las dos opciones sean distintas
                while (firstOption == secondOption)
                {
                    secondOption = Random.Range(startRange, endRange + 1);
                }

                // Asegurarnos de que el año real esté incluido en el abanico
                while (firstOption != year && secondOption != year)
                {
                    firstOption = Random.Range(startRange, endRange + 1);
                    secondOption = Random.Range(startRange, endRange + 1);
                }

                // Ordenar las dos opciones para que siempre sea primero el menor
                int smallerYear = Mathf.Min(firstOption, secondOption);
                int largerYear = Mathf.Max(firstOption, secondOption);

                // Mostrar el abanico de fechas en el cartucho
                textYear.text = $"{smallerYear % 100:00}-{largerYear % 100:00}"; // Mostramos las últimas dos cifras
            }
            else
            {
                // Si no se activan los PowerUp 2 ni 3, mostramos el año completo
                textYear.text = yearString; // Año completo
            }

            // Mostrar el texto de la fecha
            textYear.gameObject.SetActive(true);
        });
    }


    public void ActualizarFechaConAbanico()
    {
        int year = cardInfo.CardDateYear;

        // Obtén las dos últimas cifras del año
        int lastTwoDigits = year % 100;

        // Generamos un rango dentro de esa década
        int startRange = lastTwoDigits / 10 * 10; // Comienzo del rango (por ejemplo, si es 2003, comenzaría en 00)
        int endRange = startRange + 9; // Fin del rango

        // Calculamos dos números aleatorios dentro de esta década
        int firstOption = Random.Range(startRange, endRange + 1);
        int secondOption = Random.Range(startRange, endRange + 1);

        // Aseguramos que las dos opciones sean distintas
        while (firstOption == secondOption)
        {
            secondOption = Random.Range(startRange, endRange + 1);
        }

        // Mostrar el abanico de fechas en el cartucho
        string dateRange = $"{firstOption:00}-{secondOption:00}";
        textYear.text = dateRange;

        // Mostrar el cartucho con la fecha
        textYear.gameObject.SetActive(true);
    }

}