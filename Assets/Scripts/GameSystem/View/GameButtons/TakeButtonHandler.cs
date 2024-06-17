//using Events;
//using PlayersSystem;
//using UnityEngine;
//using UnityEngine.UI;

//namespace GameSystem.View.GameButtons
//{
//    [RequireComponent(typeof(Button))]
//    public class TakeButtonHandler : BaseGameButton
//    {
//        public void Init(PlayerHandler myPlayer)
//        {
//            var button = GetComponent<Button>();
//            button.onClick.RemoveAllListeners();
//            button.onClick.AddListener(() =>
//            {
//                if (!myPlayer.IsDefending) return;
//                myPlayer.HandleTakeButton();
                
//                if (PlayersContainer.Singleton.GetMovingPlayer().CardsInventoryHandler.Cards.Length == 0)
//                {
//                    var defendingPlayer = PlayersContainer.Singleton.GetDefendingPlayer();
//                    defendingPlayer.TakeCardsFromTable();
//                    GameEventsContainer.OnMoveEndAfterWin?.Invoke();
//                }
//                else
//                    GameEventsContainer.OnPlayerPressTake?.Invoke();

//                gameObject.SetActive(false);
//            });
//        }
//    }
//}