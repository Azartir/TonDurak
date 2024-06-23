using UnityEngine;

public class StateMachineController : MonoBehaviour
{
    [SerializeField] private GameObject[] stateAObjects; // ������� ��� ��������� A
    [SerializeField] private GameObject[] stateBObjects; // ������� ��� ��������� B

    [SerializeField] private float stateDuration = 5f; // ������������ ��������� � ��������

    private StateMachine stateMachine;
    private float stateTimer;

    private void Start()
    {
        stateMachine = new StateMachine();

        // ������� ��������� � ��������� �� � ������ ���������
        stateMachine.AddState("StateLoading", new State(stateAObjects, stateBObjects));
        stateMachine.AddState("StateHands", new State(stateBObjects, stateAObjects));

        // ������������� ��������� ���������
        stateMachine.ChangeState("StateLoading");
        stateTimer = stateDuration; // ������������� ������ �� ��������� ���������
    }

    private void Update()
    {
        // ��������� ������� ���������
        stateMachine.UpdateCurrentState();

        // ��������� ������ �� ����� �����
        stateTimer -= Time.deltaTime;

        // ���������, ������� �� ����� �������� ���������
        if (stateTimer <= 0f)
        {
            // ����������� �� ��������� ��������� � ���������� ������
            if (stateMachine.GetCurrentStateName() == "StateLoading")
            {
                stateMachine.ChangeState("StateHands");
            }
            stateTimer = stateDuration; // ���������� ������ �� ������������ ���������
        }
    }

    public void SetState(string stateName)
    {
        stateMachine.ChangeState(stateName);
        stateTimer = stateDuration; // ���������� ������
    }
}
