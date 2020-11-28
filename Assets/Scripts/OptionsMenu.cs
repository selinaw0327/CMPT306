using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    //public AudioMixer audioMixer;
    //public AudioSource audioSource;

    private static readonly string FristPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private int firstPlayInt;
    public Slider backgroundSlider, soundEffectsSlider;
    private float backgroundFloat, soundEffectsFloat;
    public AudioSource backgroundAudio;
    public AudioSource[] soundEffectsAudio;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FristPlay);

        if (firstPlayInt == 0)
        {
            backgroundFloat = .25f;
            soundEffectsFloat = .75f;
            backgroundSlider.value = backgroundFloat;
            soundEffectsSlider.value = soundEffectsFloat;
            PlayerPrefs.SetFloat(BackgroundPref, backgroundFloat);
            PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat);
            PlayerPrefs.SetInt(FristPlay, -1);
        } else
        {
            backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
            backgroundSlider.value = backgroundFloat;
            soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
            soundEffectsSlider.value = soundEffectsFloat;
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
    }

    void OnApplicationFocus(bool inFoucs)
    {
        if (!inFoucs)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        
        backgroundAudio.volume = backgroundSlider.value;

        for (int j = 0; j < soundEffectsAudio.Length; j++)
        {
            soundEffectsAudio[j].volume = soundEffectsSlider.value;
        }
    }


    //private float BGMvolume = 1f;

    //private void Start()
    //{
    //    audioSource.Play();
    //}

    //private void Update()
    //{
    //    audioSource.volume = BGMvolume;
    //}

    //public void UpdateVolume(float volume)
    //{
    //    BGMvolume = volume;
    //}

    //public void SetVolume (float volume)
    //{
    //    audioMixer.SetFloat("volume", volume);
    //}
}
