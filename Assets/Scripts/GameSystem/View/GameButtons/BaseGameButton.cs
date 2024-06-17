using UnityEngine;

namespace GameSystem.View.GameButtons
{
    public abstract class BaseGameButton : MonoBehaviour
    {
        [SerializeField] private GameButtonType _type;

        public void TryActivate(GameButtonType type)
            => gameObject.SetActive(type == _type);
    }
}