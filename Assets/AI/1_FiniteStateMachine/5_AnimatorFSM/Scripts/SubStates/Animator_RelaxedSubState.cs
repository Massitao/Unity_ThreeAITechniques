using UnityEngine;
using Animator_FSM;

public class Animator_RelaxedSubState : StateMachineBehaviour
{
    private AnimatorFSM animatorFSM;
    [SerializeField] private string animator_ChaseTrigger;
    [SerializeField] private string animator_AttackTrigger;


    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animatorFSM == null)
        {
            animatorFSM = animator.GetComponent<AnimatorFSM>();
        }

        Debug.Log($"Entered <color=green>Relaxed</color> Sub State");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animatorFSM.GetPlayerVisibility() && !animatorFSM.GetPlayerStatus())
        {
            switch (animatorFSM.GetRobotToPlayerDistance())
            {
                case AnimatorFSM.Distance.Far:
                    animator.SetTrigger(animator_ChaseTrigger);
                    break;

                case AnimatorFSM.Distance.Near:
                    animator.SetTrigger(animator_AttackTrigger);
                    break;
            }
        }
    }


    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        Debug.Log($"Transitioned to <color=green>Relaxed</color> Sub State. Setting this State Machine default state active");
    }
}