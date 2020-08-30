using UnityEngine;

namespace TrafficLightFSM
{
    public class TrafficLight : MonoBehaviour
    {
        [Header("Light State")]
        [SerializeField] private LightStates currentLightState;
        public enum LightStates
        {
            Red,
            Yellow,
            Green
        }

        [Space(10f)]

        [Header("Light Materials")]
        [SerializeField] private Material redLightMat;
        [SerializeField] private Material yellowLightMat;
        [SerializeField] private Material greenLightMat;
        [SerializeField] private Material unlitLightMat;

        [Space(10f)]

        [Header("Physical Lights")]
        [SerializeField] private Renderer redLight;
        [SerializeField] private Renderer yellowLight;
        [SerializeField] private Renderer greenLight;

        [Space(10f)]

        [Header("Light Times")]
        [SerializeField] private TextMesh timerTextMesh;

        [Space(10f)]

        [SerializeField] private float redLightTime = 20f;
        [SerializeField] private float yellowLightTime = 5f;
        [SerializeField] private float greenLightTime = 15f;

        [Space(10f)]

        [SerializeField] private float timer;


        private void Start()
        {
            PhysicalLightChange();
        }

        void Update()
        {
            TimerUpdate();
            StateChange();
        }

        void TimerUpdate()
        {
            timer += Time.deltaTime;

            switch (currentLightState)
            {
                case LightStates.Red:
                    timerTextMesh.text = Mathf.CeilToInt(redLightTime - timer).ToString();

                    break;

                case LightStates.Yellow:
                    timerTextMesh.text = Mathf.CeilToInt(yellowLightTime - timer).ToString();

                    break;

                case LightStates.Green:
                    timerTextMesh.text = Mathf.CeilToInt(greenLightTime - timer).ToString();

                    break;
            }
        }
        void TimerReset()
        {
            timer = 0f;
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
                    break;

                case LightStates.Yellow:
                    if (timer >= yellowLightTime)
                    {
                        SetLightState(LightStates.Red);
                    }
                    break;

                case LightStates.Green:
                    if (timer >= greenLightTime)
                    {
                        SetLightState(LightStates.Yellow);
                    }
                    break;
            }
        }
        void PhysicalLightChange()
        {
            switch (currentLightState)
            {
                case LightStates.Red:
                    redLight.material = redLightMat;
                    yellowLight.material = unlitLightMat;
                    greenLight.material = unlitLightMat;

                    break;

                case LightStates.Yellow:
                    redLight.material = unlitLightMat;
                    yellowLight.material = yellowLightMat;
                    greenLight.material = unlitLightMat;

                    break;

                case LightStates.Green:
                    redLight.material = unlitLightMat;
                    yellowLight.material = unlitLightMat;
                    greenLight.material = greenLightMat;

                    break;
            }
        }
        public void SetLightState(LightStates newLightState)
        {
            Debug.Log($"Changing <color={currentLightState.ToString().ToLower()}>{currentLightState.ToString()} Light</color> to <color={newLightState.ToString().ToLower()}>{newLightState.ToString()} Light</color>");

            TimerReset();
            currentLightState = newLightState;
            PhysicalLightChange();
        }

    }
}