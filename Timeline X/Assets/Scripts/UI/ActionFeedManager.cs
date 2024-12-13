using UnityEngine;
using TMPro;  
using System.Collections.Generic;
public class ActionFeedManager : MonoBehaviour
{
    [SerializeField] private TMP_Text feedText;  // El texto que muestra el feed
    [SerializeField] private int maxLines = 5;  // N�mero m�ximo de l�neas en el feed

    private List<string> feedEntries = new List<string>();  // Lista para almacenar las entradas del feed

    // M�todo para agregar una entrada al feed
    public void LogAction(string action)
    {
        // Agregar la nueva acci�n al inicio de la lista
        feedEntries.Insert(0, action);

        // Si el n�mero de entradas excede el l�mite, eliminar la m�s antigua
        if (feedEntries.Count > maxLines)
        {
            feedEntries.RemoveAt(feedEntries.Count - 1);
        }

        // Actualizar el texto del feed con las entradas actuales
        UpdateFeedText();
    }

    // M�todo para actualizar el texto del feed en pantalla
    private void UpdateFeedText()
    {
        feedText.text = "";  // Limpiar el texto del feed actual

        // Unir todas las entradas en un solo string con saltos de l�nea
        foreach (string entry in feedEntries)
        {
            feedText.text += entry + "\n";
        }
    }
}