//using System.Collections.Generic;
//using CardsInventorySystem.View;
//using UnityEngine;

//public class CardsLayout : MonoBehaviour
//{
//    [Header("Position")]
//    [SerializeField] private Vector2 _cardOffsetBorders;
//    [SerializeField] private Vector2 _vericalOffsetBorders;
//    [SerializeField] private float _defaultCardOffset = 10f;
  
//    [Header("Rotation")]
//    [SerializeField] private float _maxRotation = 30f;
//    [SerializeField] private float _defaultMaxVericalOffset = 20f;

//    public void LayoutCards(List<CardView> cards)
//    {
//        var cardOffset = _defaultCardOffset / cards.Count;
//        cardOffset = Mathf.Clamp(cardOffset, _cardOffsetBorders.x, _cardOffsetBorders.y);
//        var maxVerticalOffset = _defaultMaxVericalOffset * cards.Count;
//        maxVerticalOffset = Mathf.Clamp(maxVerticalOffset, _vericalOffsetBorders.x, _vericalOffsetBorders.y);
//        var centerIndex = cards.Count / 2;
//        var totalWidth = (cards.Count - 1) * cardOffset;
//        var startX = -totalWidth / 2;
//        var centerY = 0f;

//        for (int i = 0; i < cards.Count; i++)
//        {
//            var cardRect = cards[i].GetComponent<RectTransform>();
//            var positionX = startX + i * cardOffset;
//            cards[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(positionX, 0);
            
//            var rotationFactor = (float)(i - centerIndex) / centerIndex;
//            var rotationZ = rotationFactor * _maxRotation;
//            cardRect .rotation = Quaternion.Euler(0, 0, -rotationZ);
            
//            if (i == centerIndex)
//            {
//                cardRect.anchoredPosition += new Vector2(0, maxVerticalOffset);
//                centerY = cardRect.anchoredPosition.y;
//            }
//        }
        
//        for (int i = 0; i < centerIndex; i++)
//        {
//            var t = centerIndex / (float)(i + 1);
//            var verticalOffset =
//                Mathf.Pow(t, 2) * maxVerticalOffset;
//            cards[i].GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, verticalOffset);
//            cards[cards.Count - 1 - i].GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, verticalOffset);
//        }
        
//        float centerOffset = centerY - cards[centerIndex].GetComponent<RectTransform>().anchoredPosition.y;
//        for (int i = 0; i < cards.Count; i++)
//        {
//            cards[i].GetComponent<RectTransform>().anchoredPosition += new Vector2(0, centerOffset);
//        }
//    }
//}