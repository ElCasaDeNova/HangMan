using UnityEngine;

public class Tripping : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip trippingpStart;
    [SerializeField] AudioClip trippingEnd;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void TrippingStart()
    {
        audioSource.PlayOneShot(trippingpStart);
    }

    void TrippingEnd()
    {
        audioSource.PlayOneShot(trippingEnd);
    }
}
