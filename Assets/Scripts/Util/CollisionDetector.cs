using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public Animator animator;
    public string triggerName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Animation Triggered");
            // Run Animation
            animator.SetTrigger(triggerName);
        }
    }
}
