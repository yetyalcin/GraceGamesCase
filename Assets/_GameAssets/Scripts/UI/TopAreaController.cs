using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class TopAreaController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _counterText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        #region CustomMethods
        public void SetLevelText(int value)
        {
            _levelText.text = value.ToString();
        }

        public void SetCounterText(float value)
        {
            _counterText.text = value.ToString("f0");
        }

        public void SetScoreText(int value)
        {
            _scoreText.text = value.ToString();
        }
        #endregion

    }
}

