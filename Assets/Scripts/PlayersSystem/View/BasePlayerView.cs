//using MultiplayerSystem.CustomData;
//using UnityEngine;
//using UnityEngine.UI;

//namespace PlayersSystem.View
//{
//    public abstract class BasePlayerView : MonoBehaviour
//    {
//        [Header("UI")] 
//        [SerializeField] private Image _avatarIcon;
//        [SerializeField] private GameObject _activeMovePanel;
//        public PlayerHandler PlayerHandler { get; private set; }

//        public virtual void Init(PlayerHandler playerHandler)
//            => PlayerHandler = playerHandler;

//        public abstract void DisplayCards(NetworkCardData[] cards);
        
//        protected virtual void DisplayUI()
//        {
//        }
        
//        public void HandleActiveMovePanel(bool value)
//            => _activeMovePanel.SetActive(value);
//    }
//}