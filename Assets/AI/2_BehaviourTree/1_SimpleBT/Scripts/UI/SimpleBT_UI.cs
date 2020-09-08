using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SimpleBT_UI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Robot_SimpleBT bt;
    [SerializeField] private Animator spriteAnimator;

    [Space(10)]

    [Header("UI BT Node Text")]
    [SerializeField] private Text nodeText;

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
    [SerializeField] private string kickTrigger;
    [SerializeField] private string attackTrigger;

    [Header("Timer Coroutine")]
    private Coroutine timerCoroutine;


    private void OnEnable()
    {
        bt.OnStateChange += UpdateUI;
    }
    private void OnDisable()
    {
        bt.OnStateChange -= UpdateUI;
    }


    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }


    private void UpdateUI()
    {
        switch (bt.GetRobotState())
        {
            case Robot_SimpleBT.RobotStates.Idle:
                spriteAnimator.SetTrigger(idleTrigger);

                StartTimer();

                break;

            case Robot_SimpleBT.RobotStates.Patrol:
                spriteAnimator.SetTrigger(patrolTrigger);

                StopTimer();

                break;

            case Robot_SimpleBT.RobotStates.Chase:
                spriteAnimator.SetTrigger(chaseTrigger);

                StopTimer();

                break;

            case Robot_SimpleBT.RobotStates.Attack:
                spriteAnimator.SetTrigger(attackTrigger);

                StopTimer();

                break;

            case Robot_SimpleBT.RobotStates.Kick:
                spriteAnimator.SetTrigger(kickTrigger);

                StopTimer();

                break;
        }

        nodeText.text = bt.GetRobotState().ToString();

        distanceDropdown.SetValueWithoutNotify((int)bt.GetRobotToPlayerDistance());

        idlePauseTimeInputField.text = bt.GetIdlePauseTime().ToString();

        reachedPatrolPointToggle.SetIsOnWithoutNotify(bt.GetPatrolReachedPoint());

        canSeePlayerToggle.SetIsOnWithoutNotify(bt.GetPlayerVisibility());
        playerIsDeadToggle.SetIsOnWithoutNotify(bt.GetPlayerStatus());
    }

    private void StartTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(TimerCountdown());
    }
    private void StopTimer()
    {
        remainingIdleTimeTitleText.enabled = false;
        remainingIdleTimeText.enabled = false;

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }
    private IEnumerator TimerCountdown()
    {
        remainingIdleTimeTitleText.enabled = true;
        remainingIdleTimeText.enabled = true;

        while (bt.GetIdlePauseTime() - bt.GetIdlePauseTimer() > Mathf.Epsilon)
        {
            remainingIdleTimeText.text = (bt.GetIdlePauseTime() - bt.GetIdlePauseTimer()).ToString("0.00");
            yield return null;
        }

        StopTimer();

        yield break;
    }


    public void SetRobotToPlayerDistance(int newDistance)
    {
        bt.SetRobotToPlayerDistance((Robot_SimpleBT.Distance)newDistance);
    }

    public void SetIdlePauseTime(string newTime)
    {
        if (int.Parse(newTime) < 0f)
        {
            idlePauseTimeInputField.SetTextWithoutNotify(bt.GetIdlePauseTime().ToString());
            return;
        }

        bt.SetIdlePauseTime(int.Parse(newTime));
    }

    public void SetPatrolPointCheck(bool reachedPoint)
    {
        bt.SetPatrolReachedPoint(reachedPatrolPointToggle);
    }

    public void SetPlayerVisibility(bool visible)
    {
        bt.SetPlayerVisibility(visible);
    }
    public void SetPlayerStatus(bool reachedPoint)
    {
        bt.SetPlayerStatus(reachedPoint);
    }
}