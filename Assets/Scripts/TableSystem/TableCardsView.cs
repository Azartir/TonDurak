//using UnityEngine;

//namespace TableSystem
//{
//    public class TableCardsView : MonoBehaviour
//    {
//        [Header("UI")] 
//        [SerializeField] private Transform _placeForCards;
//        [SerializeField] private TableCardView _cardViewPref;

//        private void ClearPlace(Transform place)
//        {
//            foreach(Transform child in place)
//                Destroy(child.gameObject);
//        }
        
//        public void DisplayCards(NetworkTableCardData[] cards, TableCardsHandler tableCardsHandler)
//        {
//            ClearPlace(_placeForCards);
//            foreach (var card in cards)
//            {
//                var instance = Instantiate(_cardViewPref, _placeForCards);
//                instance.Init(card, tableCardsHandler);
//            }
//        }
//    }
//}