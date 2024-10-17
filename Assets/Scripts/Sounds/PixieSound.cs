using UnityEngine;

public class PixieSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip laughOne;
    [SerializeField] float laughOneVolume = 1f;
    [SerializeField] float laughOnePitch = 2f;

    [SerializeField] AudioClip laughTwo;
    [SerializeField] float laughTwoVolume = 1f;
    [SerializeField] float laughTwoPitch = 2f;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void LaughOne()
    {
        float randomVariant = Random.Range(0.8f, 1.2f);
        SetAudioSource(laughOneVolume, laughOnePitch * randomVariant);
        audioSource.PlayOneShot(laughOne);
    }

    void LaughTwo()
    {
        float randomVariant = Random.Range(0.8f, 1.2f);
        SetAudioSource(laughTwoVolume, laughTwoPitch * randomVariant);
        audioSource.PlayOneShot(laughTwo);
    }


    void SetAudioSource(float newVolume, float newPitch)
    {
        audioSource.volume = newVolume;
        audioSource.pitch = newPitch;
    }

}
