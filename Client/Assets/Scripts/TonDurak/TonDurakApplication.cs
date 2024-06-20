using UnityEngine;
using UnityWebClient;

namespace TonDurak
{
    public class TonDurakApplication : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
