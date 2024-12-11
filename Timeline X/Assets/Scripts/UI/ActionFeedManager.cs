using UnityEngine;
using TMPro;  
using System.Collections.Generic;
public class ActionFeedManager : MonoBehaviour
{
    [SerializeField] private TMP_Text feedText;  // El texto que muestra el feed
    [SerializeField] private int maxLines = 5;  // Número máximo de líneas en el feed

    private List<string> feedEntries = new List<string>();  // Lista para almacenar las entradas del feed

    // Método para agregar una entrada al feed
    public void LogAction(string action)
    {
        // Agregar la nueva acción al inicio de la lista
        feedEntries.Insert(0, action);

        // Si el número de entradas excede el límite, eliminar la más antigua
        if (feedEntries.Count > maxLines)
        {
            feedEntries.RemoveAt(feedEntries.Count - 1);
        }

        // Actualizar el texto del feed con las entradas actuales
        UpdateFeedText();
    }

    // Método para actualizar el texto del feed en pantalla
    private void UpdateFeedText()
    {
        feedText.text = "";  // Limpiar el texto del feed actual

        // Unir todas las entradas en un solo string con saltos de línea
        foreach (string entry in feedEntries)
        {
            feedText.text += entry + "\n";
        }
    }
}