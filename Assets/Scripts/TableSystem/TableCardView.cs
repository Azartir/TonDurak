//using CardsInventorySystem.View;
//using GameSystem;
//using MultiplayerSystem.CustomData;
//using UnityEngine;
//using UnityEngine.EventSystems;

//namespace TableSystem
//{
//    public class TableCardView : MonoBehaviour, IDropHandler
//    {
//        [SerializeField] private CardView _currenCardView;
//        [SerializeField] private CardView _bitCardView;

//        private NetworkTableCardData _data;
//        private TableCardsHandler _tableCardsHandler;
//        public NetworkTableCardData Data => _data;

//        public void Init(NetworkTableCardData data, TableCardsHandler tableCardsHandler)
//        {
//            _tableCardsHandler = tableCardsHandler;
//            _data = data;
//            _currenCardView.Init(_data.CurrentCardData);
//            if(_data.BitCardData.Id == -1) return;
//            _bitCardView.gameObject.SetActive(true);
//            _bitCardView.Init(_data.BitCardData);
//        }

//        public void OnDrop(PointerEventData eventData)
//        {
//            if (eventData.pointerDrag == null) return;
//            var script = eventData.pointerDrag.GetComponent<ClickableCardHandler>();
//            if(script == null) return;
//            if(!script.Player.IsDefending) return;
//            if(!_tableCardsHandler.TryBitCard(Data.CurrentCardData, new NetworkCardData(script.CardData.Value, script.CardData.Type), script.Player)) return;
//            script.Player.RemoveCard(new NetworkCardData(script.CardData.Value, script.CardData.Type));
//            script.Destroy();
//        }
//    }
//}