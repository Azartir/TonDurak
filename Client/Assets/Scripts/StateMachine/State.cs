using UnityEngine;

public class State : IState
{
    private GameObject[] objectsToActivate;
    private GameObject[] objectsToDeactivate;

    public State(GameObject[] activate, GameObject[] deactivate)
    {
        objectsToActivate = activate;
        objectsToDeactivate = deactivate;
    }

    public void Enter()
    {
        Debug.Log("Enter State");
        SetActiveObjects(objectsToActivate, true);
        SetActiveObjects(objectsToDeactivate, false);
    }

    public void Exit()
    {
        Debug.Log("Exit State");
    }

    public void Update()
    {
        // Update logic for State Main Turn
    }

    private void SetActiveObjects(GameObject[] objects, bool isActive)
    {
        foreach (var obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
            }
        }
    }
}