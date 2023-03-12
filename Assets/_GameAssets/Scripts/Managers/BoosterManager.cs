using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

namespace Game.Managers
{
    public class BoosterManager : MonoBehaviour
    {
        private static BoosterManager instance;
        public static BoosterManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<BoosterManager>(true);

                return instance;
            }
        }

        #region UnityBuildinFunctions
        private void Awake()
        {
            instance = this;
        }
        #endregion

        #region CustomMethods
        public void FreezeBooster()
        {
            Debug.Log("Freeze activated");
        }
        public void ElectrictyBooster()
        {
            Debug.Log("Electric activated");
        }
        public void MagnetBooster()
        {
            Debug.Log("Magnet activated");
        }
        public void ClockBooster()
        {
            Debug.Log("Clock activated");
        }
        #endregion

    }
}

