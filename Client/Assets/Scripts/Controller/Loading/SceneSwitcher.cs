using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [Tooltip("Время в секундах перед переключением сцены.")]
    public float delay = 5f;

    [Tooltip("Имя сцены, на которую нужно переключиться.")]
    public string sceneName;

    private void Start()
    {
        StartCoroutine(SwitchSceneAfterDelay());
    }

    private System.Collections.IEnumerator SwitchSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        if (SceneExists(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Сцена с именем '{sceneName}' не найдена.");
        }
    }

    private bool SceneExists(string name)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (sceneName == name)
            {
                return true;
            }
        }
        return false;
    }
}

