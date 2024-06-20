using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loading
{
    public class GameSceneLoading : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene(1);
        }
    }
}