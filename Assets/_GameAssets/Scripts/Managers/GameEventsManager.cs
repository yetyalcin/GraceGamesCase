using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.Events;

namespace Game.Managers
{
    public class GameEventsManager : MonoBehaviour
    {
        public static GameEventsManager Instance;
        public UnityAction EvntMergeComplete;
        public UnityAction<int> EvntSlotFilled;
        public UnityAction EvnSlotUnFill;
    
        #region UnityBuildinFunctions
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
        #region CustomMethods
    	public void MergeComplete()
        {
            EvntMergeComplete?.Invoke();
        }
        public void SlotFilled(int index)
        {
            EvntSlotFilled?.Invoke(index);
        }
        public void SlotUnFill()
        {
            EvnSlotUnFill?.Invoke();
        }
        #endregion
    }

}

