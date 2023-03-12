using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Game.UI;
using Game.Controller;

namespace Game.Managers
{
    public class GamePlayCanvasUI : MonoBehaviour
    {
        private static GamePlayCanvasUI _instance;
        public static GamePlayCanvasUI Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<GamePlayCanvasUI>(true);

                return _instance;
            }
        }

        public CardAreaController CardAreaController;
        public TopAreaController TopAreaController;
        public GridController GridController;

        #region UnityBuildinFunctions
        private void Awake()
        {
            _instance = this;
        }
        #endregion

        #region CustomMethods
        public void OnBoosterButtonClicked(int boosterNumber)
        {
            switch (boosterNumber)
            {
                case 0:
                    BoosterManager.Instance.FreezeBooster();
                    break;
                case 1:
                    BoosterManager.Instance.ElectrictyBooster();
                    break;
                case 2:
                    BoosterManager.Instance.MagnetBooster();
                    break;
                case 3:
                    BoosterManager.Instance.ClockBooster();
                    break;
                default:
                    break;
            }
        }
        #endregion
        
        private IEnumerator CorCounterTimeWithoutUpdate()
        {
            float offsetValue = 10;
            float timer = 0;
            while (timer <= offsetValue)
            {
                timer += Time.deltaTime;
                Debug.Log(timer.ToString("f1"));
                yield return new WaitForEndOfFrame();
            }
        }

        

    }
}

