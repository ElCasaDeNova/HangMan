using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown qualityDropdown;
    [SerializeField]
    private Scrollbar musicVolumeScrollbar;
    [SerializeField]
    private Scrollbar generalVolumeScrollbar;
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioClip audioClip;
    private AudioSource audioSource;
    [SerializeField]
    private float maxDBMusic = -10f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        qualityDropdown.value = QualitySettings.GetQualityLevel();

        qualityDropdown.onValueChanged.AddListener(SetQuality);
        musicVolumeScrollbar.onValueChanged.AddListener(SetMusicVolume);
        generalVolumeScrollbar.onValueChanged.AddListener(SetGeneralVolume);
    }
    public void SetQuality(int qualityIndex)
    {
        PlaySound();
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log("Quality Set to: " + qualityIndex);
    }



    public void SetMusicVolume(float scrollbarValue)
    {
        // Convert maxDB to a linear value (for the highest allowed volume)
        float maxLinearVolume = Mathf.Pow(10, maxDBMusic / 20f);

        // The scrollbarValue should be between 0 and 1 (0 = mute, 1 = maxDB)
        // Multiply the scrollbar value by the maxLinearVolume to get the final volume
        float linearVolume = scrollbarValue * maxLinearVolume;

        // Convert the linear volume to dB for the audio mixer
        float dBVolume = (linearVolume > 0.0001f) ? Mathf.Log10(linearVolume) * 20 : -80f;

        // Apply the volume to the audio mixer
        audioMixer.SetFloat("MusicVolume", dBVolume);
    }


    public void SetGeneralVolume(float volume)
    {
        float dBVolume = (volume > 0.0001f) ? Mathf.Log10(volume) * 20 : -80f;
        audioMixer.SetFloat("GeneralVolume", dBVolume);
        Debug.Log("General Volume: " + volume);
    }

    private void PlaySound()
    {
        // Give variety to sound
        float randomVariant = Random.Range(0.8f, 1.2f);
        audioSource.pitch = randomVariant;
        audioSource.PlayOneShot(audioClip);

        // Set to default
        audioSource.pitch = 1;
    }
}
