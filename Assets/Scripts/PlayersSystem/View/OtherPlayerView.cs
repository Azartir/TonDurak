//using GameSystem.View.GameButtons;
//using MultiplayerSystem.CustomData;
//using TMPro;
//using UnityEngine;

//namespace PlayersSystem.View
//{
//    public class OtherPlayerView : BasePlayerView
//    {
//        [SerializeField] private GameObject _activatingObject;
//        [SerializeField] private TMP_Text _cardsCountText;
//        [SerializeField] private OtherPlayerButtonsView _buttonsView;

//        public override void Init(PlayerHandler playerHandler)
//        {
//            _activatingObject.gameObject.SetActive(true);
//            base.Init(playerHandler);
//            _buttonsView.Init(playerHandler);
//        }
        
//        public override void DisplayCards(NetworkCardData[] cards)
//        {
//            _cardsCountText.text = cards.Length.ToString() + " cards";
//        }
//    }
//}