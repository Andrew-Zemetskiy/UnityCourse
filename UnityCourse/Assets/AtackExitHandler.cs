using UnityEngine;

public class AtackExitHandler : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsAtacking", false);
    }
}
