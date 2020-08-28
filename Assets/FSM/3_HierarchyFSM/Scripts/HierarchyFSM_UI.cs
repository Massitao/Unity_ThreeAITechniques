using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HierarchyFSM_UI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private HierarchyFSM fsm;
    [SerializeField] private Animator spriteAnimator;

    [Space(10)]

    [Header("UI FSM State Text")]
    [SerializeField] private Text groupStateText;
    [SerializeField] private Text stateText;

    [Space(10)]

    [Header("UI Input Components")]
    [SerializeField] private Dropdown distanceDropdown;

    [Space(5)]

    [SerializeField] private InputField idlePauseTimeInputField;
    [SerializeField] private Text remainingIdleTimeTitleText;
    [SerializeField] private Text remainingIdleTimeText;

    [Space(5)]

    [SerializeField] private Toggle reachedPatrolPointToggle;

    [Space(5)]

    [SerializeField] private Toggle canSeePlayerToggle;
    [SerializeField] private Toggle playerIsDeadToggle;

    [Space(10)]

    [Header("Sprite Animator Triggers")]
    [SerializeField] private string idleTrigger;
    [SerializeField] private string patrolTrigger;
    [SerializeField] private string chaseTrigger;
    [SerializeField] private string attackTrigger;

    [Header("Timer Coroutine")]
    private Coroutine timerCoroutine;


    private void OnEnable()
    {
        fsm.OnStateChange += UpdateUI;
    }
    private void OnDisable()
    {
        fsm.OnStateChange -= UpdateUI;
    }


    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }


    private void UpdateUI()
    {
        switch (fsm.GetRobotState())
        {
            case HierarchyFSM.RobotStates.Idle:
                spriteAnimator.SetTrigger(idleTrigger);

                if (timerCoroutine != null)
                {
                    StopCoroutine(timerCoroutine);
                }
                timerCoroutine = StartCoroutine(TimerCountdown());

                break;

            case HierarchyFSM.RobotStates.Patrol:
                spriteAnimator.SetTrigger(patrolTrigger);

                StopTimer();

                break;

            case HierarchyFSM.RobotStates.Chase:
                spriteAnimator.SetTrigger(chaseTrigger);

                StopTimer();

                break;

            case HierarchyFSM.RobotStates.Attack:
                spriteAnimator.SetTrigger(attackTrigger);

                StopTimer();

                break;
        }

        groupStateText.text = fsm.GetRobotGroupStates().ToString();
        stateText.text = fsm.GetRobotState().ToString();

        distanceDropdown.SetValueWithoutNotify((int)fsm.GetRobotToPlayerDistance());

        idlePauseTimeInputField.text = fsm.GetIdlePauseTime().ToString();

        reachedPatrolPointToggle.SetIsOnWithoutNotify(fsm.GetPatrolReachedPoint());

        canSeePlayerToggle.SetIsOnWithoutNotify(fsm.GetPlayerVisibility());
        playerIsDeadToggle.SetIsOnWithoutNotify(fsm.GetPlayerStatus());
    }

    private void StopTimer()
    {
        if (timerCoroutine != null)
        {
            remainingIdleTimeTitleText.enabled = false;
            remainingIdleTimeText.enabled = false;

            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }
    private IEnumerator TimerCountdown()
    {
        remainingIdleTimeTitleText.enabled = true;
        remainingIdleTimeText.enabled = true;

        while (fsm.GetIdlePauseTime() - fsm.GetIdlePauseTimer() > Mathf.Epsilon)
        {
            remainingIdleTimeText.text = (fsm.GetIdlePauseTime() - fsm.GetIdlePauseTimer()).ToString("0.00");
            yield return null;
        }

        StopTimer();

        yield break;
    }


    public void SetRobotToPlayerDistance(int newDistance)
    {
        fsm.SetRobotToPlayerDistance((HierarchyFSM.Distance)newDistance);
    }

    public void SetIdlePauseTime(string newTime)
    {
        if (int.Parse(newTime) < 0f)
        {
            idlePauseTimeInputField.SetTextWithoutNotify(fsm.GetIdlePauseTime().ToString());
            return;
        }

        fsm.SetIdlePauseTime(int.Parse(newTime));
    }

    public void SetPatrolPointCheck(bool reachedPoint)
    {
        fsm.SetPatrolReachedPoint(reachedPatrolPointToggle);
    }

    public void SetPlayerVisibility(bool visible)
    {
        fsm.SetPlayerVisibility(visible);
    }
    public void SetPlayerStatus(bool reachedPoint)
    {
        fsm.SetPlayerStatus(reachedPoint);
    }
}