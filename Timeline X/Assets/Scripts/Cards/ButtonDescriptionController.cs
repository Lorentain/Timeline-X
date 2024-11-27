using DG.Tweening;
using UnityEngine;

public class ButtonDescriptionController : MonoBehaviour
{

    [SerializeField] private Camera camera;

    [SerializeField] private GameObject canvasDescription;

    [SerializeField] private float movementTime;

    [SerializeField] private Ease movementEase;

    private void OnMouseDown()
    {
        Debug.Log("Estoi haciendo zoom");
        if (canvasDescription.activeInHierarchy)
        {
            canvasDescription.SetActive(false);
            camera.DOOrthoSize(4.5f, movementTime).SetEase(movementEase);
            camera.transform.DOMove(new Vector3(0f, 0, -10f), movementTime).SetEase(movementEase);
        }
        else
        {
            camera.DOOrthoSize(0.5f, movementTime).SetEase(movementEase).OnComplete(() =>
            {
                canvasDescription.SetActive(true);
            });
            camera.transform.DOMove(new Vector3(0f, -3.5f, -10f), movementTime).SetEase(movementEase);
        }
    }
}
