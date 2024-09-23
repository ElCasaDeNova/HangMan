using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchCollisionDetector : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Branch Triggered");
            // Run Animation
            animator.SetTrigger("LoseRound");
        }
    }
}
