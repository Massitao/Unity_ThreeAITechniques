using UnityEngine;

public class TrafficLight_TFG : MonoBehaviour
{
    public enum LightStates
    {
        Red,
        Yellow,
        Green
    }
    [SerializeField] private LightStates currentLightState;

    [Space(10f)]

    [SerializeField] private float redLightTime = 20f;
    [SerializeField] private float yellowLightTime = 5f;
    [SerializeField] private float greenLightTime = 15f;

    [Space(10f)]

    [SerializeField] private float timer;



    // Update is called once per frame
    void Update()
    {
        TimerUpdate();
        StateChange();
    }

    void TimerUpdate()
    {
        timer += Time.deltaTime;
    }

    void StateChange()
    {
        switch (currentLightState)
        {
            case LightStates.Red:

                if (timer >= redLightTime)
                {
                    SetLightState(LightStates.Green);
                }
                else
                {
                    Debug.Log($"<color=red>RED</color>");
                }
                break;

            case LightStates.Yellow:
                if (timer >= yellowLightTime)
                {
                    SetLightState(LightStates.Red);
                }
                else
                {
                    Debug.Log($"<color=yellow>YELLOW</color>");
                }
                break;

            case LightStates.Green:
                if (timer >= greenLightTime)
                {
                    SetLightState(LightStates.Yellow);
                }
                else
                {
                    Debug.Log($"<color=green>GREEN</color>");
                }
                break;
        }
    }
    public void SetLightState(LightStates newLightState)
    {
        Debug.Log($"Changing <color={currentLightState.ToString().ToLower()}>{currentLightState.ToString()} Light</color> to <color={newLightState.ToString().ToLower()}>{newLightState.ToString()} Light</color>");

        timer = 0f;
        currentLightState = newLightState;
    }
}