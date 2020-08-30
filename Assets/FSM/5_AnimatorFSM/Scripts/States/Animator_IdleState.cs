using UnityEngine;
using Animator_FSM;

public class Animator_IdleState : StateMachineBehaviour
{
    private AnimatorFSM animatorFSM;
    [SerializeField] private string animator_PatrolTrigger;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animatorFSM == null)
        {
            animatorFSM = animator.GetComponent<AnimatorFSM>();
        }

        Debug.Log($"New State: <color=green>IDLE</color>");
        animatorFSM.SetRobotState(AnimatorFSM.RobotStates.Idle);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animatorFSM.GetIdlePauseTimer() < animatorFSM.GetIdlePauseTime())
        {
            animatorFSM.SetIdlePauseTimer(animatorFSM.GetIdlePauseTimer() + Time.deltaTime);
        }
        else
        {
            animator.SetTrigger(animator_PatrolTrigger);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animatorFSM.SetIdlePauseTimer(0f);
    }
}