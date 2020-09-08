using UnityEngine;
using Animator_FSM;

public class Animator_ChaseState : StateMachineBehaviour
{
    private AnimatorFSM animatorFSM;
    [SerializeField] private string animator_AttackTrigger;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animatorFSM == null)
        {
            animatorFSM = animator.GetComponent<AnimatorFSM>();
        }

        Debug.Log($"New State: <color=orange>CHASE</color>");
        animatorFSM.SetRobotState(AnimatorFSM.RobotStates.Chase);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animatorFSM.GetRobotToPlayerDistance() == AnimatorFSM.Distance.Near)
        {
            animator.SetTrigger(animator_AttackTrigger);
        }
    }
}