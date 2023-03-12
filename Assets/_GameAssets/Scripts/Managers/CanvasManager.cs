using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    public class CanvasManager : MonoBehaviour
    {
        private static CanvasManager instance;
        public static CanvasManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<CanvasManager>(true);

                return instance;
            }
        }

        [SerializeField] private List<GameObject> _canvasList;
        private GameObject _activeCanvas;

        #region UnityBuildinFunctions
        private void Awake()
        {
            instance = this;
            SetActiveCanvas(0);
        }
        #endregion

        #region CustomMethods
        public void SetActiveCanvas(int canvasIndex)
        {
            foreach (GameObject canvas in _canvasList)
            {
                canvas.SetActive(false);
            }

            _activeCanvas = _canvasList[canvasIndex];
            _activeCanvas.SetActive(true);
        }
        public GameObject GetActiveCanvas()
        {
            return _activeCanvas;
        }

        public void OnClickEndGame(bool isWin)
        {
            if (isWin)
            {
                Debug.Log("Load next level");
            }
            else
            {
                Debug.Log("Reload current level");
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        #endregion
    }
}


