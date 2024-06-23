using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<string, IState> states = new Dictionary<string, IState>();
    private IState currentState;
    private string currentStateName;

    public void AddState(string name, IState state)
    {
        states[name] = state;
    }

    public void ChangeState(string name)
    {
        if (states.TryGetValue(name, out IState newState))
        {
            currentState?.Exit();
            currentState = newState;
            currentStateName = name;
            currentState.Enter();
        }
        else
        {
            throw new Exception($"State {name} does not exist in the state machine.");
        }
    }

    public void UpdateCurrentState()
    {
        currentState?.Update();
    }

    public string GetCurrentStateName()
    {
        return currentStateName;
    }
}