using DG.Tweening;
using UnityEngine;

public class ButtonDescriptionController : MonoBehaviour
{

    [SerializeField] private Camera camera;

    private void OnMouseDown() {
        Debug.Log("Estoi haciendo zoom");
        camera.DOOrthoSize(0.5f,10f);
    }
}
