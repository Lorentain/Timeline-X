using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource effectsSource;

    [SerializeField] private AudioClip correctCardEffectClip;
    [SerializeField] private AudioClip incorrectCardEffectClip;
    [SerializeField] private AudioClip finishGameEffectClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Se destruye el objeto completo, no solo el script
        }
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // Silenciar
        {
            audioMixer.SetFloat("BGVolume", -80);
            audioMixer.SetFloat("EffectsVolume", -80);
        }

        if (Input.GetKeyDown(KeyCode.U)) // Restaurar volumen
        {
            audioMixer.SetFloat("BGVolume", 0);
            audioMixer.SetFloat("EffectsVolume", 0);
        }
    }

    private void PlayBackgroundMusic()
    {
        if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }

    public static void PlayCorrectCardEffect()
    {
        instance.PlayEffect(instance.correctCardEffectClip);
    }

    public static void PlayIncorrectCardEffect()
    {
        instance.PlayEffect(instance.incorrectCardEffectClip);
    }

    public static void PlayFinishGameEffect() {
        instance.PlayEffect(instance.finishGameEffectClip);
    }

    private void PlayEffect(AudioClip clip)
    {
        if (effectsSource != null && clip != null)
        {
            effectsSource.PlayOneShot(clip); // Usa PlayOneShot para no interrumpir otros sonidos
        }
    }
}
