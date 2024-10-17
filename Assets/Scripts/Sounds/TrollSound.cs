using UnityEngine;

public class TrollSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip getUp;
    [SerializeField] float getUpVolume = 1f;
    [SerializeField] float getUpPitch = 0.2f;

    [SerializeField] AudioClip hello;
    [SerializeField] float helloVolume = 1f;
    [SerializeField] float helloPitch = 0.2f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void GetUp()
    {
        SetAudioSource(getUpVolume, getUpPitch);
        audioSource.PlayOneShot(getUp);
    }

    void Hello()
    {
        SetAudioSource(helloVolume, helloPitch);
        audioSource.PlayOneShot(hello);
    }

    void SetAudioSource(float newVolume, float newPitch)
    {
        audioSource.volume = newVolume;
        audioSource.pitch = newPitch;
    }

}
