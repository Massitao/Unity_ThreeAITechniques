using UnityEngine;
using Animator_FSM;

public class Animator_InitialSelector : StateMachineBehaviour
{
    private AnimatorFSM animatorFSM;

    [SerializeField] private string animator_IdleTrigger;
    [SerializeField] private string animator_PatrolTrigger;
    [SerializeField] private string animator_ChaseTrigger;
    [SerializeField] private string animator_AttackTrigger;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animatorFSM == null)
        {
            animatorFSM = animator.GetComponent<AnimatorFSM>();
        }

        switch (animatorFSM.GetRobotState())
        {
            case AnimatorFSM.RobotStates.Idle:
                animator.SetTrigger(animator_IdleTrigger);

                break;

            case AnimatorFSM.RobotStates.Patrol:
                animator.SetTrigger(animator_PatrolTrigger);

                break;

            case AnimatorFSM.RobotStates.Chase:
                animator.SetTrigger(animator_ChaseTrigger);

                break;

            case AnimatorFSM.RobotStates.Attack:
                animator.SetTrigger(animator_AttackTrigger);

                break;
        }
    }
}