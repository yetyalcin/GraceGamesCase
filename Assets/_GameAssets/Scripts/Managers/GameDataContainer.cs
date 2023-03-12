using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using System.Linq;
using Game.Core;


namespace Game.Managers
{
    public class GameDataContainer : MonoBehaviour
    {
        private static GameDataContainer instance;
        public static GameDataContainer Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<GameDataContainer>(true);

                return instance;
            }
        }
        public List<ItemInfo> ItemInfo;
        private void Awake()
        {
             instance = this;
        }

        private void Start()
        {
            
        }

        #region CustomMethods
        public Sprite GetItemSpriteByItem(int itemIndex)
        {
            return ItemInfo.Find(item => item.ID == itemIndex).ItemSprite;
        }

        public Item3D GetItemById(int itemIndex)
        {
            return ItemInfo.Find(item => item.ID == itemIndex).Item;
        }
        #endregion



    }
}

