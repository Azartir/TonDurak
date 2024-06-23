using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameSystem.View
{
    public class WinView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _winText;
        [SerializeField] private Button _continueButton;

        public void Init(bool isWin)
        {
            _panel.gameObject.SetActive(true);
            string textStr = string.Empty;
            if (isWin)
                textStr = "You Win";
            else
                textStr = "You Loose";
            _winText.text = textStr;
            _continueButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        }
    }
}