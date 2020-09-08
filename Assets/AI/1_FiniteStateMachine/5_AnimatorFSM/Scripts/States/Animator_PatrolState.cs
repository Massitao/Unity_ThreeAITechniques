using UnityEngine;
using Animator_FSM;

public class Animator_PatrolState : StateMachineBehaviour
{
    private AnimatorFSM animatorFSM;
    [SerializeField] private string animator_IdleTrigger;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animatorFSM == null)
        {
            animatorFSM = animator.GetComponent<AnimatorFSM>();
        }

        Debug.Log($"New State: <color=yellow>Patrol</color>");
        animatorFSM.SetRobotState(AnimatorFSM.RobotStates.Patrol);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animatorFSM.GetPatrolReachedPoint())
        {
            animatorFSM.SetPatrolReachedPoint(false);
            animator.SetTrigger(animator_IdleTrigger);
        }
    }
}