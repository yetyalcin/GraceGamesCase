using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using TMPro;

namespace Game.UI
{
    public class BoosterUI : MonoBehaviour
    {
        public GlobalVariables.BoosterType BoosterType;
        [SerializeField] private TextMeshProUGUI _boosterAmountText;

        #region CustomMethods
        public void SetBoosterAmountText(int value)
        {
            _boosterAmountText.text = value.ToString();
        }
        #endregion
    }
}

