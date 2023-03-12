using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Game.Managers;
using Game.Core;
using Game.UI;

namespace Game.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask _rayMask;

        private GridController _gridController;
        private List<Transform> _currentMovingItems = new List<Transform>();

        #region UnityBuildinFunctions
        private void Start()
        {
            _gridController = GamePlayCanvasUI.Instance.GridController;
        }
        private void Update()
        {
            InteractWithClick();
        }
        #endregion

        #region CustomMethods
        public void ReArange(List<Transform> list)
        {
            foreach (GridPoint item in _gridController.GridPoints)
            {
                item.UnFillGrid();
            }

            foreach (Transform item in list)
            {
                Item3D currentInteract3D = item.GetComponent<Item3D>();
                InteractWithClickOvr(currentInteract3D);
            }
            foreach (var item in _currentMovingItems)
            {
                DestroyImmediate(item.gameObject);
            }

            _currentMovingItems.Clear();
        }
        private void InteractWithClick()
        {
            if (!GamePlayManager.Instance.GameState.Equals(GlobalVariables.GameStates.Run))
                return;

            if (_currentMovingItems.Count > 0  )
                return;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                bool rayCast = Physics.Raycast(GetMouseRay(), out hit, _rayMask);

                if (rayCast)
                {
                    if (hit.transform.gameObject.layer.Equals(GlobalVariables.ItemLayerIndex))
                    {
                        Item3D clickedItem = hit.transform.GetComponent<Item3D>();
                        GridPoint moveTarget = null;
                        GridPoint selectedGridPoint = null;

                        CalculateMatchedItems(clickedItem, ref moveTarget, ref selectedGridPoint);
                        _currentMovingItems.Add(clickedItem.transform);
                        clickedItem.SetItemSelected(moveTarget, false);
                    }
                }
            }
        }
        private void InteractWithClickOvr(Item3D clickedItem)
        {
            GridPoint moveTarget = null;
            GridPoint selectedGridPoint = null;

            CalculateMatchedItems(clickedItem, ref moveTarget, ref selectedGridPoint);

            clickedItem.SetItemSelected(moveTarget, true);
        }
        private void CalculateMatchedItems(Item3D clickedItem, ref GridPoint moveTarget, ref GridPoint selectedGridPoint)
        {
            if (GamePlayCanvasUI.Instance.GridController.IsAllGridEmpty())
            {
                selectedGridPoint = _gridController.GetFirstEmptyGridPoint();
                moveTarget = selectedGridPoint;
            }
            else if (_gridController.IsAnyMatchOnGridPoints(clickedItem.GetItemIndex()) == null && !_gridController.IsAllGridPointsFull())
            {
                selectedGridPoint = _gridController.GetFirstEmptyGridPoint();
                moveTarget = selectedGridPoint;
            }
            else if (_gridController.IsAnyMatchOnGridPoints(clickedItem.GetItemIndex()))
            {
                int j = _gridController.IsAnyMatchOnGridPoints(clickedItem.GetItemIndex()).GetGridIndex();

                if (_gridController.GetFirstEmptyGridPoint() != null && j + 1 == _gridController.GetFirstEmptyGridPoint().GetGridIndex())
                {
                    selectedGridPoint = _gridController.GetFirstEmptyGridPoint();
                    moveTarget = selectedGridPoint;
                }
                else
                {
                    GridPoint gridpoint = _gridController.IsAnyMatchOnGridPoints(clickedItem.GetItemIndex());
                    int matchedIndex = gridpoint.GetGridIndex();
                    int selectedGridSlotIndex;

                    selectedGridSlotIndex = matchedIndex + 1;

                    _gridController.SlideGridPointsToEnd(selectedGridSlotIndex);

                    selectedGridPoint = _gridController.GridPoints[selectedGridSlotIndex];
                    moveTarget = selectedGridPoint;
                }
            }
        }
        public void RemoveCurrentMoveList(Transform moveItem)
        {
            _currentMovingItems.Remove(moveItem);
        }
        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        #endregion
    }
}


