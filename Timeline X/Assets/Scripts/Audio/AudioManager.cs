using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField] private AudioMixer audioMixer;

    void Awake()
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            audioMixer.SetFloat("BGVolume", -80);
            audioMixer.SetFloat("EffectsVolume", -80);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            audioMixer.SetFloat("BGVolume", 0);
            audioMixer.SetFloat("EffectsVolume", 0);
        }
    }
}
