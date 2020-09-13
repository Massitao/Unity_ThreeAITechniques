using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UtilityAI_UI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RobotUtilityBlackboard utilityAIBlackboard;

    [Space(10)]

    [Header("UI Utility AI Current Action Text")]
    [SerializeField] private Text actionText;

    [Space(10)]

    [Header("UI Input Components")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Text currentHealthText;
    [SerializeField] private Text minimumHealthText;
    [SerializeField] private Text maximumHealthText;

    [Space(5)]

    [SerializeField] private Slider distanceSlider;
    [SerializeField] private Text currentDistanceText;
    [SerializeField] private Text minimumDistanceText;
    [SerializeField] private Text maximumDistanceText;

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


    [Header("Timer Coroutine")]
    private Coroutine timerCoroutine;


    private void OnEnable()
    {
        utilityAIBlackboard.OnAnyChange += UpdateUI;
    }
    private void OnDisable()
    {
        utilityAIBlackboard.OnAnyChange -= UpdateUI;
    }


    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }


    private void UpdateUI()
    {
        actionText.text = utilityAIBlackboard.currentDecisionName;

        if (actionText.text == "Idling")
        {
            StartTimer();
        }
        else
        {
            StopTimer();
        }

        healthSlider.SetValueWithoutNotify(utilityAIBlackboard.GetRobotHealth());
        currentHealthText.text = $"{utilityAIBlackboard.GetRobotHealth().ToString()}";

        distanceSlider.SetValueWithoutNotify(utilityAIBlackboard.GetRobotToPlayerDistance());
        currentDistanceText.text = $"{utilityAIBlackboard.GetRobotToPlayerDistance().ToString()} m";

        idlePauseTimeInputField.text = utilityAIBlackboard.GetIdlePauseTime().ToString();

        reachedPatrolPointToggle.SetIsOnWithoutNotify(utilityAIBlackboard.GetPatrolReachedPoint());

        canSeePlayerToggle.SetIsOnWithoutNotify(utilityAIBlackboard.GetPlayerVisibility());
        playerIsDeadToggle.SetIsOnWithoutNotify(utilityAIBlackboard.GetPlayerStatus());
    }

    public void SetRobotHealth(float newHealth)
    {
        utilityAIBlackboard.SetRobotHealth(newHealth);
    }

    public void SetDistance(float newDistance)
    {
        utilityAIBlackboard.SetRobotToPlayerDistance(newDistance);
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

        while (utilityAIBlackboard.GetIdlePauseTime() - utilityAIBlackboard.GetIdlePauseTimer() > Mathf.Epsilon)
        {
            remainingIdleTimeText.text = (utilityAIBlackboard.GetIdlePauseTime() - utilityAIBlackboard.GetIdlePauseTimer()).ToString("0.00");
            yield return null;
        }

        StopTimer();

        yield break;
    }

    public void SetIdlePauseTime(string newTime)
    {
        if (int.Parse(newTime) < 0f)
        {
            idlePauseTimeInputField.SetTextWithoutNotify(utilityAIBlackboard.GetIdlePauseTime().ToString());
            return;
        }

        utilityAIBlackboard.SetIdlePauseTime(int.Parse(newTime));
    }

    public void SetPatrolPointCheck(bool reachedPoint)
    {
        utilityAIBlackboard.SetPatrolReachedPoint(reachedPoint);
    }

    public void SetPlayerVisibility(bool visible)
    {
        utilityAIBlackboard.SetPlayerVisibility(visible);
    }
    public void SetPlayerStatus(bool dead)
    {
        utilityAIBlackboard.SetPlayerStatus(dead);
    }
}