using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class TimelineController : MonoBehaviour
{
    private static TimelineController instance;

    [SerializeField] private List<GameObject> cardsTimeline;

    [SerializeField] private DeckController deckController;

    [SerializeField] private GameObject prefabCard;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementEase;

    [SerializeField] private bool animationPlay = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cardsTimeline = new List<GameObject>();
    }

    public static void PonerCartaInicial()
    {
        // Obtiene una carta del mazo llamando al método RepartirCarta del controlador del mazo
        CardInfo aux = instance.deckController.RepartirCarta();

        // Crea una nueva instancia de la carta usando el prefab configurado y la asocia al GameObject del timeline
        CardController card = Instantiate(instance.prefabCard, instance.gameObject.transform).GetComponent<CardController>();

        // Asigna la información de la carta obtenida (aux) al controlador de la carta recién creada
        card.AgregarCardInfo(aux);

        // Establece el texto del año de la carta, utilizando la información contenida en CardInfo
        card.gameObject.GetComponent<CardController>().AgregarAñoCardInfo(
            card.gameObject.GetComponent<CardController>().ObtenerCardInfo()
        );

        // Coloca la carta recién creada en el centro (posición local en el timeline)
        card.transform.localPosition = new Vector3(0, 0, 0);

        // Añade la carta al timeline llamando al método correspondiente
        AñadirCartaTimeline(card.transform.gameObject);

        // Encuentra y destruye el botón de la carta que tiene la etiqueta "Button Destroy",
        // ya que no será necesario en este estado inicial
        Destroy(card.gameObject.transform.Find("Button Destroy").gameObject);
    }

    public static bool AñadirCartaTimeline(GameObject gameObject)
    {
        // Variable auxiliar para indicar si la carta se pudo añadir correctamente al timeline
        bool aux = false;

        // Índice que almacenará la posición de la última carta en el timeline
        int positionLastCard = 0;

        // Verifica si ya hay cartas en el timeline
        if (instance.cardsTimeline.Count != 0)
        {
            // Recorre todas las cartas que ya están en el timeline
            for (int i = 0; i < instance.cardsTimeline.Count; i++)
            {
                // Si la carta actual está posicionada en el eje X mayor o igual a 0
                if (instance.cardsTimeline[i].transform.position.x >= 0)
                {
                    // Mueve la carta un espacio hacia la derecha en el eje X con una animación
                    instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 1, instance.movementTime // Duración de la animación
                    ).SetEase(instance.movementEase).OnComplete(() =>
                    {
                        // La animación finaliza, pero no se realiza ninguna acción adicional
                    });
                }

                // Si la carta actual está en la posición X = 0, almacena su índice
                if (instance.cardsTimeline[i].transform.position.x == 0)
                {
                    positionLastCard = i;
                }
            }

            // Inserta la nueva carta en el índice de la última carta detectada
            instance.cardsTimeline.Insert(positionLastCard, gameObject);

            // Indica que la carta fue añadida con éxito
            aux = true;
        }
        else
        {
            // Si no hay cartas en el timeline, simplemente añade esta como la primera
            instance.cardsTimeline.Add(gameObject);

            // Muestra un mensaje de depuración para indicar que se agregó la primera carta
            Debug.Log("EYYY");

            // Indica que la carta fue añadida con éxito
            aux = true;
        }

        // Devuelve el estado de éxito
        return aux;
    }

    public static void EliminarCartaTimeline(GameObject gameObject)
    {
        int index = instance.cardsTimeline.IndexOf(gameObject);
        instance.cardsTimeline.Remove(gameObject);
        Debug.Log(index);
        for (int i = 0; i < instance.cardsTimeline.Count; i++)
        {
            if (i >= index) // Comprueba si el indice de la carta es mayor o igual 
            {
                //instance.animationPlay = true;
                instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x - 1, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
                {
                    //instance.animationPlay = false;
                });
            }
            else if (index == instance.cardsTimeline.Count && !instance.animationPlay) // 
            {
                //instance.animationPlay = true;
                instance.cardsTimeline[i].transform.DOMoveX(instance.cardsTimeline[i].transform.position.x + 1, instance.movementTime).SetEase(instance.movementEase).OnComplete(() =>
                {
                    //instance.animationPlay = false;
                });
            }
        }
    }

    public static void MoverDerechaCartaTimeline(GameObject cardGameObject)
    {
        // Verifica que haya cartas en el timeline y que no haya animaciones en progreso
        if (instance.cardsTimeline.Count != 0 && !instance.animationPlay)
        {
            Debug.Log("Aquiii"); // Mensaje de depuración para indicar que se activó el método

            // Verifica si la carta no está en la última posición del timeline
            if (instance.cardsTimeline.IndexOf(cardGameObject) != (instance.cardsTimeline.Count - 1))
            {
                // Recorre todas las cartas en el timeline
                for (int i = 0; i < instance.cardsTimeline.Count; i++)
                {
                    // Si la carta actual está justo después de la carta que se desea mover
                    if (i == (instance.cardsTimeline.IndexOf(cardGameObject) + 1))
                    {
                        // Aumenta temporalmente el orden de renderizado para que la carta quede en primer plano
                        cardGameObject.GetComponent<SortingGroup>().sortingOrder += 1;

                        // Marca que hay una animación en progreso
                        instance.animationPlay = true;

                        // Mueve esta carta dos unidades hacia la izquierda
                        instance.cardsTimeline[i].transform.DOMoveX(
                            instance.cardsTimeline[i].transform.position.x - 2, // Nueva posición X
                            instance.movementTime // Tiempo de la animación
                        ).SetEase(instance.movementEase).OnComplete(() =>
                        {
                            // Marca que la animación terminó y restaura el orden de renderizado
                            instance.animationPlay = false;
                            cardGameObject.GetComponent<SortingGroup>().sortingOrder -= 1;
                        });
                    }
                    // Para todas las demás cartas que no son la que se está moviendo
                    else if (instance.cardsTimeline[i] != cardGameObject)
                    {
                        // Aumenta temporalmente el orden de renderizado
                        cardGameObject.GetComponent<SortingGroup>().sortingOrder += 1;

                        // Marca que hay una animación en progreso
                        instance.animationPlay = true;

                        // Mueve estas cartas una unidad hacia la izquierda
                        instance.cardsTimeline[i].transform.DOMoveX(
                            instance.cardsTimeline[i].transform.position.x - 1, // Nueva posición X
                            instance.movementTime // Tiempo de la animación
                        ).SetEase(instance.movementEase).OnComplete(() =>
                        {
                            // Marca que la animación terminó y restaura el orden de renderizado
                            instance.animationPlay = false;
                            cardGameObject.GetComponent<SortingGroup>().sortingOrder -= 1;
                        });
                    }
                }

                // Obtiene el índice actual de la carta que se desea mover
                int index = instance.cardsTimeline.IndexOf(cardGameObject);

                // Inserta la carta en su nueva posición (dos lugares más adelante en el timeline)
                instance.cardsTimeline.Insert(index + 2, cardGameObject);

                // Elimina la carta de su posición original
                instance.cardsTimeline.RemoveAt(index);
            }
        }
    }

    public static void MoverIzquierdaCartaTimeline(GameObject cardGameObject)
    {
        // Verifica que haya cartas en el timeline y que no haya animaciones en progreso
        if (instance.cardsTimeline.Count != 0 && !instance.animationPlay)
        {
            Debug.Log("Aquiii"); // Mensaje de depuración para indicar que se activó el método

            // Verifica si la carta no está en la primera posición del timeline
            if (instance.cardsTimeline.IndexOf(cardGameObject) != 0)
            {
                // Recorre todas las cartas en el timeline
                for (int i = 0; i < instance.cardsTimeline.Count; i++)
                {
                    // Si la carta actual está justo antes de la carta que se desea mover
                    if (i == (instance.cardsTimeline.IndexOf(cardGameObject) - 1))
                    {
                        // Aumenta temporalmente el orden de renderizado para que la carta quede en primer plano
                        cardGameObject.GetComponent<SortingGroup>().sortingOrder += 1;

                        // Marca que hay una animación en progreso
                        instance.animationPlay = true;

                        // Mueve esta carta dos unidades hacia la derecha
                        instance.cardsTimeline[i].transform.DOMoveX(
                            instance.cardsTimeline[i].transform.position.x + 2, // Nueva posición X
                            instance.movementTime // Tiempo de la animación
                        ).SetEase(instance.movementEase).OnComplete(() =>
                        {
                            // Marca que la animación terminó y restaura el orden de renderizado
                            instance.animationPlay = false;
                            cardGameObject.GetComponent<SortingGroup>().sortingOrder -= 1;
                        });
                    }
                    // Para todas las demás cartas que no son la que se está moviendo
                    else if (instance.cardsTimeline[i] != cardGameObject)
                    {
                        // Aumenta temporalmente el orden de renderizado
                        cardGameObject.GetComponent<SortingGroup>().sortingOrder += 1;

                        // Marca que hay una animación en progreso
                        instance.animationPlay = true;

                        // Mueve estas cartas una unidad hacia la derecha
                        instance.cardsTimeline[i].transform.DOMoveX(
                            instance.cardsTimeline[i].transform.position.x + 1, // Nueva posición X
                            instance.movementTime // Tiempo de la animación
                        ).SetEase(instance.movementEase).OnComplete(() =>
                        {
                            // Marca que la animación terminó y restaura el orden de renderizado
                            instance.animationPlay = false;
                            cardGameObject.GetComponent<SortingGroup>().sortingOrder -= 1;
                        });
                    }
                }

                // Obtiene el índice actual de la carta que se desea mover
                int index = instance.cardsTimeline.IndexOf(cardGameObject);

                // Inserta la carta en su nueva posición (una posición antes en el timeline)
                instance.cardsTimeline.Insert(index - 1, cardGameObject);

                // Elimina la carta de su posición original (una posición más adelante)
                instance.cardsTimeline.RemoveAt(index + 1);
            }
        }
    }

    public static bool ComprobarCarta(GameObject card)
    {
        bool res = false;

        // Obtiene el índice de la carta dentro de la lista cardsTimeline
        int index = instance.cardsTimeline.IndexOf(card);

        // Verifica si la carta siguiente (a la derecha) tiene un año menor que la carta actual
        if (index != instance.cardsTimeline.Count - 1 &&
            instance.cardsTimeline[index].gameObject.GetComponent<CardController>().ObtenerAñoCarta() >
            instance.cardsTimeline[index + 1].gameObject.GetComponent<CardController>().ObtenerAñoCarta())
        {
            // Imprime en consola que la carta está mal ubicada (a la derecha)
            Debug.Log("MAL DERECHA");
            Debug.Log("Mi:" + instance.cardsTimeline[index].gameObject.GetComponent<CardController>().ObtenerAñoCarta());
            Debug.Log("Derecha:" + instance.cardsTimeline[index + 1].gameObject.GetComponent<CardController>().ObtenerAñoCarta());

            // Si la carta está mal, la mueve a su posición correcta
            MoverCartaALugarCorrecto(card);

            // Llama a RoundManager para confirmar que el movimiento fue incorrecto
            RoundManager.ConfirmPlay(false);

            // Roba una carta del inventario
            CardInventory cardInventory = card.GetComponent<CardController>().ObtenerInventario();
            cardInventory.RobarCarta();
        }
        // Verifica si la carta anterior (a la izquierda) tiene un año mayor que la carta actual
        else if (index != 0 &&
                 instance.cardsTimeline[index].gameObject.GetComponent<CardController>().ObtenerAñoCarta() <
                 instance.cardsTimeline[index - 1].gameObject.GetComponent<CardController>().ObtenerAñoCarta())
        {
            // Imprime en consola que la carta está mal ubicada (a la izquierda)
            Debug.Log("MAL IZQUIERDA");
            Debug.Log("Mi:" + instance.cardsTimeline[index].gameObject.GetComponent<CardController>().ObtenerAñoCarta());
            Debug.Log("Izquierda:" + instance.cardsTimeline[index - 1].gameObject.GetComponent<CardController>().ObtenerAñoCarta());

            // Si la carta está mal, la mueve a su posición correcta
            MoverCartaALugarCorrecto(card);

            // Llama a RoundManager para confirmar que el movimiento fue incorrecto
            RoundManager.ConfirmPlay(false);

            // Roba una carta del inventario
            CardInventory cardInventory = card.GetComponent<CardController>().ObtenerInventario();
            cardInventory.RobarCarta();

            // Imprime en consola los detalles de la carta izquierda
            Debug.Log("Izquierda:" + instance.cardsTimeline[index - 1].gameObject.GetComponent<CardController>().ObtenerAñoCarta());
        }
        // Si las cartas adyacentes están bien (la carta actual está entre dos cartas con años más grandes y pequeños)
        else
        {
            // Imprime en consola que la carta está correctamente colocada
            Debug.Log("BIEN");

            // Marca que la carta está correctamente ubicada
            res = true;

            // Llama a RoundManager para confirmar que el movimiento fue correcto
            RoundManager.ConfirmPlay(true);
        }

        // Devuelve si la carta está correctamente ubicada o no
        return res;
    }

    public static void ComprobarCartaYParpadear(GameObject card)
    {
        // Llama a la función ComprobarCarta() para verificar si la carta está en la posición correcta.
        bool esCorrecta = ComprobarCarta(card);

        // Establece el color de la carta dependiendo de si está correcta o no:
        // Verde si está correcta, rojo si está incorrecta.
        Color colorFinal = esCorrecta ? Color.green : Color.red;

        // Asegúrate de que la carta tiene un componente SpriteRenderer (para modificar su color).
        SpriteRenderer spriteRenderer = card.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Si la carta tiene un SpriteRenderer, realiza un parpadeo de color utilizando DOTween:

            // Cambia el color de la carta a 'colorFinal' (verde o rojo) durante 0.2 segundos.
            spriteRenderer.DOColor(colorFinal, 0.2f)
                .OnComplete(() =>
                {
                    // Después del primer parpadeo, regresa el color a blanco (o al color original de la carta).
                    spriteRenderer.DOColor(Color.white, 0.2f);
                });
        }
    }

    private static void MoverCartaALugarCorrecto(GameObject card)
    {
        // Obtiene el año de la carta actual.
        int añoCarta = card.GetComponent<CardController>().ObtenerAñoCarta();

        // Inicializa la variable para almacenar la posición correcta, por defecto es -1.
        int posicionCorrecta = -1;

        // Buscar la posición correcta en la timeline:
        for (int i = 0; i < instance.cardsTimeline.Count; i++)
        {
            // Obtiene el año de la carta en la posición actual de la timeline.
            int añoActual = instance.cardsTimeline[i].GetComponent<CardController>().ObtenerAñoCarta();

            // Si el año de la carta es menor que el año de la carta en la timeline, 
            // encontramos la posición correcta para insertar la carta.
            if (añoCarta < añoActual)
            {
                posicionCorrecta = i;  // La posición correcta será antes de esta carta.
                break;  // Salimos del bucle una vez que encontramos la posición.
            }
        }

        // Si no encontró ninguna carta con un año mayor, significa que la carta debe ir al final.
        if (posicionCorrecta == -1)
            posicionCorrecta = instance.cardsTimeline.Count;

        // Mueve la carta a la posición correcta en la timeline.
        MoverCarta(card, posicionCorrecta);
    }

    private static void MoverCarta(GameObject card, int nuevaPosicion)
    {
        // Obtiene la posición actual de la carta en la timeline
        int posicionActual = instance.cardsTimeline.IndexOf(card);

        // Si la carta ya está en la posición correcta, no hace nada.
        if (nuevaPosicion == posicionActual)
            return;

        // Elimina la carta de la posición actual de la timeline.
        instance.cardsTimeline.RemoveAt(posicionActual);

        // Si la nueva posición está después de la carta removida, ajustamos la posición,
        // ya que después de remover la carta, las cartas posteriores se mueven una posición hacia atrás.
        if (nuevaPosicion > posicionActual)
            nuevaPosicion--;  // Ajusta la posición si la carta se movió a un índice superior.

        // Inserta la carta en la nueva posición en la timeline.
        instance.cardsTimeline.Insert(nuevaPosicion, card);

        // Actualiza las posiciones físicas de todas las cartas para reflejar su nueva posición en la timeline.
        for (int i = 0; i < instance.cardsTimeline.Count; i++)
        {
            GameObject carta = instance.cardsTimeline[i];

            // Mueve cada carta a su nueva posición en el eje X, utilizando DOTween para animar el movimiento.
            carta.transform.DOMoveX(i, instance.movementTime).SetEase(instance.movementEase);
        }
    }

    public static Vector3 TimelinePosicion()
    {
        return instance.transform.position;
    }

    public static Transform TimelineTransform()
    {
        return instance.transform;
    }

    public static bool GetAnimationPlay()
    {
        return instance.animationPlay;
    }
}