using UnityEngine;

public class WalkingLoopBehaviour : StateMachineBehaviour
{
    // Called everytime WalkingLoop is called
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Fix Axe Y Rotation to 90
        Vector3 currentRotation = animator.transform.rotation.eulerAngles;
        animator.transform.rotation = Quaternion.Euler(currentRotation.x, 90, currentRotation.z);
    }
}
