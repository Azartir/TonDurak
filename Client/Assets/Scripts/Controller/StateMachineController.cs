using UnityEngine;

public class StateMachineController : MonoBehaviour
{
    [SerializeField] private GameObject[] stateAObjects; // Объекты для состояния A
    [SerializeField] private GameObject[] stateBObjects; // Объекты для состояния B

    [SerializeField] private float stateDuration = 5f; // Длительность состояния в секундах

    private StateMachine stateMachine;
    private float stateTimer;

    private void Start()
    {
        stateMachine = new StateMachine();

        // Создаем состояния и добавляем их в машину состояний
        stateMachine.AddState("StateLoading", new State(stateAObjects, stateBObjects));
        stateMachine.AddState("StateHands", new State(stateBObjects, stateAObjects));

        // Устанавливаем начальное состояние
        stateMachine.ChangeState("StateLoading");
        stateTimer = stateDuration; // Устанавливаем таймер на начальное состояние
    }

    private void Update()
    {
        // Обновляем текущее состояние
        stateMachine.UpdateCurrentState();

        // Уменьшаем таймер на время кадра
        stateTimer -= Time.deltaTime;

        // Проверяем, истекло ли время текущего состояния
        if (stateTimer <= 0f)
        {
            // Переключаем на следующее состояние и сбрасываем таймер
            if (stateMachine.GetCurrentStateName() == "StateLoading")
            {
                stateMachine.ChangeState("StateHands");
            }
            stateTimer = stateDuration; // Сбрасываем таймер на длительность состояния
        }
    }

    public void SetState(string stateName)
    {
        stateMachine.ChangeState(stateName);
        stateTimer = stateDuration; // Сбрасываем таймер
    }
}
