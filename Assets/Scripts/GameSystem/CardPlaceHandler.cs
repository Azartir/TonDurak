//using UnityEngine;
//using UnityEngine.EventSystems;

//namespace GameSystem
//{
//    public class CardPlaceHandler : MonoBehaviour, IDropHandler
//    {
//        public void OnDrop(PointerEventData eventData)
//        {
//            if (eventData.pointerDrag == null) return;
//            var script = eventData.pointerDrag.GetComponent<ClickableCardHandler>();
//            if(script == null) return;
//            script.TryPlaceOnTable();
//        }
//    }
//}