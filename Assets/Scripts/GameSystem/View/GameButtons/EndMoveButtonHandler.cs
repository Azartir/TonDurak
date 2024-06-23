//using Events;
//using PlayersSystem;
//using SaveSystem;
//using TableSystem;
//using UnityEngine;
//using UnityEngine.UI;

//namespace GameSystem.View.GameButtons
//{
//    [RequireComponent(typeof(Button))]
//    public class EndMoveButtonHandler : BaseGameButton
//    {
//        public void Init(PlayerHandler myPlayer)
//        {
//            var button = GetComponent<Button>();
//            button.onClick.RemoveAllListeners();
//            button.onClick.AddListener(() =>
//            {
//                if (!myPlayer.IsMoving && !myPlayer.IsThrows) return;
//                myPlayer.HandlePassButton();
//                myPlayer.PassPressed = true;
//                if (!TakingCardsHandler.Singleton.PlayerPressedTake)
//                {
//                    if (GameTypeMarker.Singleton.GameType == GameType.Passing)
//                    {
//                        if(PlayersContainer.Singleton.AllPayersPass())
//                            GameEventsContainer.OnPlayerPressPass?.Invoke();
//                    }
//                    else
//                        GameEventsContainer.OnPlayerPressPass?.Invoke();
//                }
//                else
//                {
//                    if (GameTypeMarker.Singleton.GameType == GameType.Passing)
//                    {
//                        if(PlayersContainer.Singleton.AllPayersPass())
//                        {
//                            var defendingPlayer1 = PlayersContainer.Singleton.GetDefendingPlayer();
//                            defendingPlayer1.TakeCardsFromTable();
//                            GameEventsContainer.OnPlayerPressedPassAfterTake?.Invoke();
//                        }
//                    }
//                    else
//                    {
//                        var defendingPlayer = PlayersContainer.Singleton.GetDefendingPlayer();
//                        defendingPlayer.TakeCardsFromTable();
//                        GameEventsContainer.OnPlayerPressedPassAfterTake?.Invoke();
//                    }
//                }
                 
//                gameObject.SetActive(false);
//            });
//        }
//    }
//}