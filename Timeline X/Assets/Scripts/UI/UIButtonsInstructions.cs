using UnityEngine;

public class UIButtonsInstructions : MonoBehaviour
{
    [SerializeField] private GameObject currentInstruction;

    [SerializeField] private GameObject nextInstruction;

    [SerializeField] private GameObject previousInstruction;

    public void ButtonNext()
    {
        currentInstruction.SetActive(false);
        nextInstruction.SetActive(true);
    }

    public void ButtonPrevious()
    {
        currentInstruction.SetActive(false);
        previousInstruction.SetActive(true);
    }
}
