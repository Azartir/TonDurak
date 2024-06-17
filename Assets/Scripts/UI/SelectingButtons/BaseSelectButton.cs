//using UnityEngine;
//using UnityEngine.UI;

//namespace UI.SelectingButtons
//{
//    [RequireComponent(typeof(Button))]
//    public abstract class BaseSelectButton : MonoBehaviour
//    {
//        [Header("BaseSelectingButton")] 
//        [SerializeField] private GameObject _activePanel;
//        [SerializeField] private GameObject _unactivePanel;

//        public Button Button { get; set; }

//        protected RoomCreator RoomCreator;

//        private void Awake()
//            => Button = GetComponent<Button>();
        
//        public void Init(RoomCreator roomCreator)
//            => RoomCreator = roomCreator;
        
//        public virtual void TryActivate(BaseSelectButton btn)
//        {
//            _activePanel.SetActive(btn == this);
//            _unactivePanel.SetActive(btn != this);
//        }
//    }
//}