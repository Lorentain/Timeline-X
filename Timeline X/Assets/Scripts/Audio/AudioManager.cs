using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    [SerializeField] private AudioMixer mixer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float volume;
        mixer.GetFloat("BG Volume", out volume);
        
        if (Input.GetKey(KeyCode.J))
        {
            mixer.SetFloat("BG Volume", -80);
        }

        if (Input.GetKey(KeyCode.K))
        {
            mixer.SetFloat("BG Volume", 0);
        }
    }
}
