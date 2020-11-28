using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource audioSource;

    private float BGMvolume = 1f;

    private void Start()
    {
        audioSource.Play();
    }

    private void Update()
    {
        audioSource.volume = BGMvolume;
    }

    public void UpdateVolume(float volume)
    {
        BGMvolume = volume;
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
