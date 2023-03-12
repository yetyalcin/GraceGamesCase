using UnityEngine;
using TMPro;

namespace Game.Managers
{
    public class MainMenuCanvasUI : MonoBehaviour
    {
        private static MainMenuCanvasUI _instance;
        public static MainMenuCanvasUI Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<MainMenuCanvasUI>(true);

                return _instance;
            }
        }
        [SerializeField] private TextMeshProUGUI _lifeCountText;

        private void Awake()
        {
            _instance = this;
        }

        #region CustomMethods
        public void OnClickStart()
        {
            GamePlayManager.Instance.GameState = GlobalVariables.GameStates.Run;
            SpawnManager.Instance.CanSpawnItems = true;
            CanvasManager.Instance.SetActiveCanvas(1);
        }
        public void DecreeseLifeCount(int decreeseAmount)
        {
            GamePlayManager.Instance.LifeCount -= decreeseAmount;
            _lifeCountText.text = GamePlayManager.Instance.LifeCount.ToString();
        }
        #endregion
    }
}


