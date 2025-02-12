using UnityEngine;
using unitySM = UnityEngine.SceneManagement.SceneManager;
using unityLSM = UnityEngine.SceneManagement.LoadSceneMode;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance;

    [SerializeField] private string firstScene;

    private string loadScene;

    [SerializeField] private CanvasGroup fadeCanvas;

    [SerializeField] private float fadeTime;

    [SerializeField] private Image fillLoadingBar;

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
        float progress = 0;
        do
        {
            if (progress >= 0.5f)
            {
                yield return null;
                progress += Time.deltaTime;
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(0.5f,1f));
                progress += Random.Range(0.05f, 0.2f);
            }
            Debug.Log(asynOP.progress);
            fillLoadingBar.fillAmount = progress;
        } while (progress < 1 || !asynOP.isDone);
        progress = 0;
        instance.loadScene = sceneName;
        yield return instance.fadeCanvas.DOFade(0, instance.fadeTime).OnComplete(() =>
        {
            fadeCanvas.interactable = false;
            fadeCanvas.blocksRaycasts = false;
            fillLoadingBar.fillAmount = 0;
        }).WaitForCompletion();
    }
}
