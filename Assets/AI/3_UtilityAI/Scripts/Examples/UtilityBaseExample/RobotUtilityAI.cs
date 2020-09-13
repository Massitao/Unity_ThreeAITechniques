using System.Collections;
using UnityEngine;

public class RobotUtilityAI : MonoBehaviour
{
    [Header("Robot Components")]
    [SerializeField] private Reasoner utilityAI;
    [SerializeField] private RobotUtilityBlackboard utilityBlackboard;
    [SerializeField] private Animator robotAnimator;

    private Coroutine idleCoroutine;
    private Coroutine attackCoroutine;
    private Coroutine healCoroutine;

    [Header("Decisions")]
    [SerializeField] private Decision idlingDecision;
    [SerializeField] private Decision patrolDecision;
    [SerializeField] private Decision chaseDecision;
    [SerializeField] private Decision attackDecision;
    [SerializeField] private Decision fleeDecision;
    [SerializeField] private Decision healDecision;

    [Header("Appraisals")]
    [SerializeField] private FloatAppraisal robotHealthAppraisal;
    [SerializeField] private BooleanAppraisal isPatrollingAppraisal;
    [SerializeField] private BooleanAppraisal playerVisibilityAppraisal;
    [SerializeField] private FloatAppraisal playerDistanceAppraisal;


    [Header("Robot Animation Triggers")]
    [SerializeField] private string animator_TriggerIdle;
    [SerializeField] private string animator_TriggerPatrol;
    [SerializeField] private string animator_TriggerChase;
    [SerializeField] private string animator_TriggerAttack;
    [SerializeField] private string animator_TriggerFlee;
    [SerializeField] private string animator_TriggerHeal;



    private void OnEnable()
    {
        #region Decision Events
        idlingDecision.actionToDoWhenEntering += EnterIdling;
        idlingDecision.actionToDo += Idling;
        idlingDecision.actionToDoIfInterrupted += InterruptIdling;

        patrolDecision.actionToDoWhenEntering += EnterPatrolling;
        patrolDecision.actionToDo += Patrolling;
        patrolDecision.actionToDoIfInterrupted += InterruptPatrolling;

        chaseDecision.actionToDoWhenEntering += EnterChase;
        chaseDecision.actionToDo += Chase;

        attackDecision.actionToDoWhenEntering += EnterAttack;
        attackDecision.actionToDo += Attack;

        fleeDecision.actionToDoWhenEntering += EnterFlee;
        fleeDecision.actionToDo += Flee;

        healDecision.actionToDoWhenEntering += EnterHeal;
        healDecision.actionToDo += Heal;
        healDecision.actionToDoIfInterrupted += InterruptHeal;
        #endregion

        #region Appraisal Events
        utilityBlackboard.OnRobotHealthChange += HealthAppraisal;
        utilityBlackboard.OnPatrollingChange += IsPatrollingAppraisal;
        utilityBlackboard.OnPlayerVisibilityChange += PlayerVisionAppraisal;
        utilityBlackboard.OnCurrentDistanceChange += PlayerToRobotDistanceAppraisal;
        #endregion

        utilityAI.OnDecisionChange += LogNewDecision;
    }
    private void OnDisable()
    {
        #region Decision Events
        idlingDecision.actionToDoWhenEntering -= EnterIdling;
        idlingDecision.actionToDo -= Idling;
        idlingDecision.actionToDoIfInterrupted -= InterruptIdling;

        patrolDecision.actionToDoWhenEntering -= EnterPatrolling;
        patrolDecision.actionToDo -= Patrolling;
        patrolDecision.actionToDoIfInterrupted -= InterruptPatrolling;

        chaseDecision.actionToDoWhenEntering -= EnterChase;
        chaseDecision.actionToDo -= Chase;

        attackDecision.actionToDoWhenEntering -= EnterAttack;
        attackDecision.actionToDo -= Attack;

        fleeDecision.actionToDoWhenEntering -= EnterFlee;
        fleeDecision.actionToDo -= Flee;

        healDecision.actionToDoWhenEntering -= EnterHeal;
        healDecision.actionToDo -= Heal;
        healDecision.actionToDoIfInterrupted -= InterruptHeal;
        #endregion

        #region Appraisal Events
        utilityBlackboard.OnRobotHealthChange -= HealthAppraisal;
        utilityBlackboard.OnPatrollingChange -= IsPatrollingAppraisal;
        utilityBlackboard.OnPlayerVisibilityChange -= PlayerVisionAppraisal;
        utilityBlackboard.OnCurrentDistanceChange -= PlayerToRobotDistanceAppraisal;
        #endregion

        utilityAI.OnDecisionChange -= LogNewDecision;
    }


    // Start is called before the first frame update
    void Start()
    {
        HealthAppraisal();
        IsPatrollingAppraisal();
        PlayerVisionAppraisal();
        PlayerToRobotDistanceAppraisal();

        utilityAI.decisionsToEvaluate.Add(idlingDecision);
        utilityAI.decisionsToEvaluate.Add(patrolDecision);
        utilityAI.decisionsToEvaluate.Add(chaseDecision);
        utilityAI.decisionsToEvaluate.Add(attackDecision);
        utilityAI.decisionsToEvaluate.Add(fleeDecision);
        utilityAI.decisionsToEvaluate.Add(healDecision);

        utilityAI.InitializeUtilityAI();
    }

    #region Update Appraisals Methods
    private void HealthAppraisal()
    {
        robotHealthAppraisal.SetStartFloatAppraisal(1, utilityBlackboard.GetRobotMaxHealth(), utilityBlackboard.GetRobotHealth());
    }

    private void IsPatrollingAppraisal()
    {
        isPatrollingAppraisal.SetBoolAppraisal(utilityBlackboard.GetPatrolling());
    }

    private void PlayerVisionAppraisal()
    {
        playerVisibilityAppraisal.SetBoolAppraisal(utilityBlackboard.GetPlayerVisibility());
    }

    private void PlayerToRobotDistanceAppraisal()
    {
        playerDistanceAppraisal.SetStartFloatAppraisal(utilityBlackboard.GetCloseDistance(), utilityBlackboard.GetFarDistance(), utilityBlackboard.GetRobotToPlayerDistance());
    }
    #endregion

    #region Utility Actions
    private void EnterIdling()
    {
        robotAnimator.SetTrigger(animator_TriggerIdle);

        if (idleCoroutine != null)
        {
            StopCoroutine(idleCoroutine);
        }
        idleCoroutine = StartCoroutine(IdleBehaviour());
    }
    private bool Idling()
    {
        if (idleCoroutine == null)
        {
            utilityBlackboard.SetPatrolling(true);

            return true;
        }
        else
        {
            return false;
        }
    }
    private IEnumerator IdleBehaviour()
    {
        while (utilityBlackboard.GetIdlePauseTimer() < utilityBlackboard.GetIdlePauseTime())
        {
            utilityBlackboard.SetIdlePauseTimer(utilityBlackboard.GetIdlePauseTimer() + Time.deltaTime);

            yield return null;
        }

        utilityBlackboard.SetIdlePauseTimer(0f);
        idleCoroutine = null;
        yield break;
    }
    private void InterruptIdling()
    {
        if (idleCoroutine != null)
        {
            StopCoroutine(idleCoroutine);
            idleCoroutine = null;
        }

        utilityBlackboard.SetIdlePauseTimer(0f);
    }

    private void EnterPatrolling()
    {
        robotAnimator.SetTrigger(animator_TriggerPatrol);
    }
    private bool Patrolling()
    {
        if (utilityBlackboard.GetPatrolReachedPoint())
        {
            utilityBlackboard.SetPatrolReachedPoint(false);
            utilityBlackboard.SetPatrolling(false);
            return true;
        }
        else
        {
            return false;
        }
    }
    private void InterruptPatrolling()
    {
        utilityBlackboard.SetPatrolling(false);
    }

    private void EnterChase()
    {
        robotAnimator.SetTrigger(animator_TriggerChase);
    }
    private bool Chase()
    {
        return false;
    }

    private void EnterAttack()
    {
        robotAnimator.SetTrigger(animator_TriggerAttack);

        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        attackCoroutine = StartCoroutine(AttackBehaviour());
    }
    private bool Attack()
    {
        if (attackCoroutine == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private IEnumerator AttackBehaviour()
    {
        float attackTimer = 0f;

        while (attackTimer < utilityBlackboard.GetAttackTime())
        {
            attackTimer += Time.deltaTime;

            yield return null;
        }

        attackCoroutine = null;
        yield break;
    }

    private void EnterFlee()
    {
        robotAnimator.SetTrigger(animator_TriggerFlee);
    }
    private bool Flee()
    {
        return false;
    }

    private void EnterHeal()
    {
        robotAnimator.SetTrigger(animator_TriggerHeal);

        if (healCoroutine != null)
        {
            StopCoroutine(healCoroutine);
        }
        healCoroutine = StartCoroutine(HealBehaviour());
    }
    private bool Heal()
    {
        if (healCoroutine == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private IEnumerator HealBehaviour()
    {
        float healAmmount = 50f;
        float healTime = 5f;

        while (utilityBlackboard.GetRobotHealth() < utilityBlackboard.GetRobotMaxHealth())
        {
            yield return new WaitForSeconds(healTime);

            utilityBlackboard.SetRobotHealth(utilityBlackboard.GetRobotHealth() + healAmmount);
        }

        healCoroutine = null;
        yield break;
    }
    private void InterruptHeal()
    {
        if (healCoroutine != null)
        {
            StopCoroutine(healCoroutine);
            healCoroutine = null;
        }
    }
    #endregion

    void LogNewDecision(string decisionName)
    {
        Debug.Log(decisionName);
        utilityBlackboard.currentDecisionName = decisionName;
        utilityBlackboard.OnAnyChange?.Invoke();
    }
}