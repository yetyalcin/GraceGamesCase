using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using DG.Tweening;
using Game.UI;
using Game.Managers;

namespace Game.Core
{
    public class Item3D : MonoBehaviour
    {
        [SerializeField] private int _itemIndex;
        private Outline _outline;
        private bool _isSelected;

        private void Start()
        {
            _outline = GetComponent<Outline>();
            SetOutlineVisible(false);
        }

        #region CustomMethods
        public void SetOutlineVisible(bool isActive)
        {
            if(!isActive)
                _outline.OutlineWidth = 0;
            else
                _outline.OutlineWidth = 2;
        }

        public int GetItemIndex()
        {
            return _itemIndex;
        }
        public void SetItemSelected(GridPoint moveTarget, bool selectedWithoutClick)
        {
            if (!selectedWithoutClick)
            {
                if (_isSelected)
                    return;
                _isSelected = true;

                SetOutlineVisible(true);

                this.transform.DOScale(this.transform.localScale * 1.5f,0.5f).SetEase(Ease.OutBounce).OnComplete(() => 
                {
                    StartMoveAction(moveTarget);
                });
            }
            else
            {
                ReachedTarget(moveTarget.gameObject);
            }
        }

        private void StartMoveAction(GridPoint moveTarget)
        {
            Mover mover = GetComponent<Mover>();
            mover.MoveTween(moveTarget.gameObject);
        }

        public void ReachedTarget(GameObject target)
        {
            GridPoint gridPoint = target.GetComponent<GridPoint>();
            GridController gridController = gridPoint.GetComponentInParent<GridController>();

            this.gameObject.SetActive(false);

            gridPoint.FillGrid(_itemIndex);
            gridPoint.SetHasReached(true);
            gridController.DeleteMatchedItems();
            gridController.SlideGridPointsToEnd(_itemIndex);

            GamePlayManager.Instance.GetPlayerController().RemoveCurrentMoveList(this.transform);

            if (gridController.IsAllGridPointsFull())
            {
                DecreeseLife();
                GamePlayManager.Instance.SetGameToEnd();
            }

        }
        private void DecreeseLife()
        {
            MainMenuCanvasUI.Instance.DecreeseLifeCount(1);
        }
        #endregion

    }
}
