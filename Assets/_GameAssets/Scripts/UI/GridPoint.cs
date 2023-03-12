using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Game.Core;
using UnityEngine.UI;
using Game.Managers;

namespace Game.UI
{
    public class GridPoint : MonoBehaviour
    {
        [SerializeField] private int _index;

        private Image _image;

        [SerializeField] private int _filledItemIndex = -1;
        [SerializeField] private bool _isFilled;

       
        public bool HasItemReached;
        public bool Reserved;

        #region UnityBuildinFunctions
        private void Start()
        {
            _image = GetComponent<Image>();
            _image.enabled = false;
            _filledItemIndex = -1;

            GameEventsManager.Instance.EvntSlotFilled += FillGrid;
            GameEventsManager.Instance.EvnSlotUnFill += UnFillGrid;
        }
        #endregion

        #region CustomMethods
        public int GetGridIndex()
        {
            return _index;
        }
        public int GetFilledItemIndex()
        {
            return _filledItemIndex;
        }
        public bool IsFilled()
        {
            return _isFilled;
        }
        public void ChangeActivationImage(bool value)
        {
            _image.enabled = value;
        }
        public void SetHasReached(bool value)
        {
            HasItemReached = value;
        }
        public void FillGrid(int index)
        {
            if (index != -1)
            {
                _isFilled = true;
                _filledItemIndex = index;
                _image.sprite = GameDataContainer.Instance.GetItemSpriteByItem(index);

                ChangeActivationImage(true);
            }
            else
                UnFillGrid();
        }
        public void UnFillGrid()
        {
            _isFilled = false;
            ChangeActivationImage(false);
            _image.sprite = null;
            _filledItemIndex = -1;
        }
        #endregion
    }

}