using UnityEngine;
using UnityEngine.UI; 

public class MusicVolumeSlider : MonoBehaviour
{

    public Slider musicSlider;
    public AudioSource musicAudioSource;

    void Start()
    {
     
        musicSlider.value = 1f; 
        musicAudioSource.volume = musicSlider.value; 

 
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetMusicVolume(float volume)
    {
        // Nos aseguramos de que el AudioSource exista antes de intentar cambiar su volumen
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = volume;
        }
    }
}

