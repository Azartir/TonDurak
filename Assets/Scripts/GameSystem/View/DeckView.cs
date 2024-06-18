using MultiplayerSystem.CustomData;
using TMPro;
using UnityEngine;

namespace GameSystem.View
{
    public class DeckView : MonoBehaviour
    {
        //[SerializeField] private CardView _majorCardView;
        [SerializeField] private GameObject _displayingObj;
        [SerializeField] private GameObject _majorCardObj;
        [SerializeField] private TMP_Text _countText;

        public void Init(int cardsCount, NetworkCardData majorCardData, bool majorCardDealed)
        {
            _displayingObj.gameObject.SetActive(cardsCount > 0);
            _majorCardObj.gameObject.SetActive(!majorCardDealed);
            //_majorCardView.Init(majorCardData);
            _countText.text = cardsCount.ToString();
        }
    }
}