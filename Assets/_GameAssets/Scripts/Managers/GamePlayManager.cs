using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Game.Controller;

namespace Game.Managers
{
    public class GamePlayManager : MonoBehaviour
    {
        public GlobalVariables.GameStates GameState;

        private static GamePlayManager _instance;
        public static GamePlayManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<GamePlayManager>(true);

                return _instance;
            }
        }

        [SerializeField] private float _levelCounterTime;
        [SerializeField] private int _targetFoodCount;
        public int LifeCount;
        private PlayerController _playerController;

        #region UnityBuildinFunctions
        private void Awake()
        {
            _instance = this;
            GameState = GlobalVariables.GameStates.Start;
            _playerController = FindObjectOfType<PlayerController>();
        }
        private void Update()
        {
            if (GameState.Equals(GlobalVariables.GameStates.Run))
            {
                SetCounterTime();
            }
        }
        #endregion

        #region CustomMethods
        private void SetCounterTime()
        {
            float time = _levelCounterTime -= Time.deltaTime;

            if (time >= 0)
                GamePlayCanvasUI.Instance.TopAreaController.SetCounterText(time);
            else
            {
                SetGameToEnd();
            }
        }
        public int GetTargetFoodCount()
        {
            return _targetFoodCount;
        }
        public PlayerController GetPlayerController()
        {
            return _playerController;
        }
        public void SetGameToEnd()
        {
            CanvasManager.Instance.SetActiveCanvas(3);
            GamePlayManager.Instance.GameState = GlobalVariables.GameStates.End;
        }
        #endregion
    }
}


