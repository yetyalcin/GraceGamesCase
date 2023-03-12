using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Game.Core;

namespace Game.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        private static SpawnManager _instance;
        public static SpawnManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<SpawnManager>(true);

                return _instance;
            }
        }

        public int MaxSpawnCount;
        public bool CanSpawnItems;
        [Space(10)]
        [SerializeField] private List<Item3D> _spawnTransforms;
        [SerializeField] private Transform _spawnParent;
        [SerializeField] [Range(1, 10)] private float _spawnRadius;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private List<Item3D> _spawnedItems;

        private List<Item3D> _targetItems = new List<Item3D>();
        private List<int> _targetItemsSpawnCount = new List<int>();
        [SerializeField] private int _targetCount;

        #region UnityBuildinFunctions
        private void Awake()
        {
            _instance = this;

            foreach (var item in GameDataContainer.Instance.ItemInfo)
            {
                _spawnTransforms.Add(item.Item);
            }
        }

        private void Start()
        {
            int targetIndex = 0;
            for (int i = 0; i < _targetCount; i++)
            {
                targetIndex = Random.Range(0, _spawnTransforms.Count);
                if (!_targetItems.Contains(_spawnTransforms[targetIndex]))
                {
                    Item3D selectedItem = _spawnTransforms[targetIndex];
                    _targetItems.Add(selectedItem);
                    _targetItemsSpawnCount.Add(0);
                    _spawnTransforms.Remove(selectedItem);
                }
                else
                {
                    i--;
                }
            }

            for (int i = 0; i < GamePlayCanvasUI.Instance.CardAreaController.CardItems.Count; i++)
            {
                Sprite targetSprite = GameDataContainer.Instance.GetItemSpriteByItem(_targetItems[i].GetItemIndex());
                GamePlayCanvasUI.Instance.CardAreaController.CardItems[i].SetCardSprite(targetSprite);
                GamePlayCanvasUI.Instance.CardAreaController.CardItems[i].SetTargetCount(GamePlayManager.Instance.GetTargetFoodCount());
                GamePlayCanvasUI.Instance.CardAreaController.CardItems[i].SetItemIndex(_targetItems[i].GetItemIndex());
            }
        }
        private void Update()
        {
            if (CanSpawnItems)
            {
                CanSpawnItems = false;
                SpawnWhile(MaxSpawnCount);
                StartCoroutine(FallTheItems());
            }
        }
        #endregion

        #region CustomMethods

        public void SpawnWhile(int maxSpawnCount)
        {
            int spawnCount = MaxSpawnCount - (_targetItems.Count * 6);

            for (int i = 0; i < _targetItems.Count; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Item3D selectedItem = _targetItems[i];
                    Item3D spawnedItem = Instantiate(selectedItem, _spawnParent);

                    spawnedItem.transform.position = Random.insideUnitSphere * _spawnRadius + _spawnParent.position;
                    _spawnedItems.Add(spawnedItem);
                }
            }
            spawnCount = spawnCount - (spawnCount % 3);

            for (int i = 0; i < spawnCount / 3; i++)
            {
                int rndmNmbr = Random.Range(0, _spawnTransforms.Count);
                for (int j = 0; j < 3; j++)
                {
                    Item3D selectedItem = _spawnTransforms[rndmNmbr];
                    Item3D spawnedItem = Instantiate(selectedItem, _spawnParent);
                    spawnedItem.transform.position = Random.insideUnitSphere * _spawnRadius + _spawnParent.position;
                    _spawnedItems.Add(spawnedItem);
                }
            }
        }

        public IEnumerator FallTheItems()
        {
            for (int i = 0; i < _spawnedItems.Count; i++)
            {
                _spawnedItems[i].gameObject.SetActive(true);

                yield return new WaitForSeconds(_spawnDelay);
            }
        }
        #endregion
    }
}


