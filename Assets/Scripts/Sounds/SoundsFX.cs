using UnityEngine;

public class SoundsFX : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip hitWood;
    [SerializeField] float hitWoodVolume = 1f;
    [SerializeField] float hitWoodPitch = 1f;


    [SerializeField] AudioClip fall;
    [SerializeField] float fallVolume = 1f;
    [SerializeField] float fallPitch = 1f;

    [SerializeField] AudioClip step;
    [SerializeField] float stepVolume = 0.3f;
    [SerializeField] float stepPitch = 1f;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void HitWood()
    {
        SetAudioSource(hitWoodVolume, hitWoodPitch);
        audioSource.PlayOneShot(hitWood);
    }

    void Fall()
    {
        SetAudioSource(fallVolume, fallPitch);
        audioSource.PlayOneShot(fall);
    }

    void Step()
    {
        float randomVariant = Random.Range(0.8f, 1.2f);
        SetAudioSource(stepVolume, stepPitch * randomVariant);
        audioSource.PlayOneShot(step);
    }

    void SetAudioSource(float newVolume, float newPitch)
    {
        audioSource.volume = newVolume;
        audioSource.pitch = newPitch;
    }

}
