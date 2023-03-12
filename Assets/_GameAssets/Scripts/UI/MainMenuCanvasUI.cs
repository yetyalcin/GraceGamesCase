using UnityEngine;

namespace Game.Managers
{
    public class MainMenuCanvasUI : MonoBehaviour
    {
        #region CustomMethods
        public void OnClickStart()
        {
            GamePlayManager.Instance.GameState = GlobalVariables.GameStates.Run;
            SpawnManager.Instance.CanSpawnItems = true;
            CanvasManager.Instance.SetActiveCanvas(1);
        }
        #endregion
    }
}


