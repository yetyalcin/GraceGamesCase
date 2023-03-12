using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.UI
{
    public class CardItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _cardUIAmountText;
        [SerializeField] private Image _cardImage;
        private int _targetCount;
        private int _itemIndex;

        #region UnityBuildinFunctions
        private void Start()
        {

        }
        private void Update()
        {

        }
        #endregion

        #region CustomMethods
        public void SetCardSprite(Sprite sprite)
        {
            _cardImage.sprite = sprite;
        }
        public void SetItemIndex(int index)
        {
            _itemIndex = index;
        }
        public void SetTargetCount(int value)
        {
            _targetCount = value;
            _cardUIAmountText.text = value.ToString();
        }
        public Sprite GetItemSprite()
        {
            return _cardImage.sprite;
        }
        public int GetItemIndex()
        {
            return _itemIndex;
        }
        public int GetTargetCount()
        {
            return _targetCount;
        }
        public void ComplateCardTarget()
        {
            this.transform.DOScale(this.transform.localScale * 1.5f, 0.25f).OnComplete(() => 
            { 
                this.gameObject.SetActive(false); 
            });
            
        }
        #endregion
    }
}


