using System.Collections.Generic;
using Events;
using UnityEngine;

namespace GameSystem.View.GameButtons
{
    public class OtherPlayerButtonsView : MonoBehaviour
    {
        [SerializeField] private List<BaseGameButton> _buttons = new List<BaseGameButton>();

        //private PlayerHandler _playerHandler;

        private void OnEnable()
            => GameEventsContainer.OnNextPlayerAssigned += DisableButtons;

        //private void OnDisable()
        //{
        //    GameEventsContainer.OnNextPlayerAssigned -= DisableButtons;
        //    if (_playerHandler == null) return;
        //    _playerHandler.OnPressedTake -= DisplayTakeButton;
        //    _playerHandler.OnPressPass -= DisplayPassButton;
        //}

        //public void Init(PlayerHandler playerHandler)
        //{
        //    _playerHandler = playerHandler;
        //    _playerHandler.OnPressedTake += DisplayTakeButton;
        //    _playerHandler.OnPressPass += DisplayPassButton;
        //}

        private void DisplayPassButton()
            => DisplayButton(GameButtonType.Pass);

        private void DisplayTakeButton()
            => DisplayButton(GameButtonType.Take);

        private void DisableButtons()
            => DisplayButton(GameButtonType.None);

        private void DisplayButton(GameButtonType type)
        {
            foreach (var button in _buttons)
                button.TryActivate(type);
        }
    }
}