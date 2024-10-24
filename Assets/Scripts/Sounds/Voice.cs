using UnityEngine;

public class Voice : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip maleEffortsUp;
    [SerializeField] float maleEffortsUpVolume = 1f;
    [SerializeField] float maleEffortsUpPitch = 1f;

    [SerializeField] AudioClip maleEffortsDown;
    [SerializeField] float maleEffortsDownVolume = 1f;
    [SerializeField] float maleEffortsDownPitch = 1f;

    [SerializeField] AudioClip sigh;
    [SerializeField] float sighVolume = 1f;
    [SerializeField] float sighPitch = 1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void MaleEffortUp()
    {
        SetAudioSource(maleEffortsUpVolume, maleEffortsUpPitch);
        audioSource.PlayOneShot(maleEffortsUp);
    }

    void MaleEffortDown()
    {
        SetAudioSource(maleEffortsDownVolume, maleEffortsDownPitch);
        audioSource.PlayOneShot(maleEffortsDown);
    }
    void Sigh()
    {
        SetAudioSource(sighVolume, sighPitch);
        audioSource.PlayOneShot(sigh);
    }

    void SetAudioSource(float newVolume, float newPitch)
    {
        audioSource.volume = newVolume;
        audioSource.pitch = newPitch;
    }
}
