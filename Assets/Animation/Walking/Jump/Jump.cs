using UnityEngine;

public class Jump : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip jumpStart;
    [SerializeField] AudioClip jumpEnd;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void JumpStart()
    {
        audioSource.PlayOneShot(jumpStart);
    }

    void JumpEnd()
    {
        audioSource.PlayOneShot(jumpEnd);
    }
}
