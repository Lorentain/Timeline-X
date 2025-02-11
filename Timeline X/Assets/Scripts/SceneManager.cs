using UnityEngine;
using unitySM = UnityEngine.SceneManagement.SceneManager;
using unityLSM = UnityEngine.SceneManagement.LoadSceneMode;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance;

    [SerializeField] private string firstScene;

    private string loadScene;

    [SerializeField] private CanvasGroup fadeCanvas;

    [SerializeField] private float fadeTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        LoadScene(firstScene);
    }

    public static void LoadScene(string sceneName)
    {
        instance.StartCoroutine(instance.LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        fadeCanvas.interactable = true;
        fadeCanvas.blocksRaycasts = true;
        yield return instance.fadeCanvas.DOFade(1, instance.fadeTime).WaitForCompletion();
        // Comprueba si hay escena cargada para descargarla
        if (!string.IsNullOrEmpty(instance.loadScene))
        {
            yield return unitySM.UnloadSceneAsync(instance.loadScene);
        }
        AsyncOperation asynOP = unitySM.LoadSceneAsync(sceneName, unityLSM.Additive);
        do {
            Debug.Log(asynOP.progress);
            yield return null;
        }while(!asynOP.isDone);
        
        instance.loadScene = sceneName;
        yield return instance.fadeCanvas.DOFade(0, instance.fadeTime).OnComplete(() => {
            fadeCanvas.interactable = false;
            fadeCanvas.blocksRaycasts = false;
        }).WaitForCompletion();
    }
}
