using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using Game.Managers;
using UnityEngine.Events;
using System;

namespace Game.UI
{
    public class GridController : MonoBehaviour
    {
        public List<GridPoint> GridPoints;

        #region UnityBuildinFunctions

        public void Start()
        {
            GameEventsManager.Instance.EvntMergeComplete += this.OnMergeComplete;
        }
        public void OnDestroy()
        {
            GameEventsManager.Instance.EvntMergeComplete -= this.OnMergeComplete;
        }
        #endregion

        #region CustomMethods
        private void OnMergeComplete()
        {
            this.ReArange();
        }
        #endregion

        public bool IsAllGridEmpty()
        {
            return GridPoints.Count(item => item.IsFilled()) == 0;
        }
        public bool IsAllGridPointsFull()
        {
            return GridPoints.Count(item => item.IsFilled()) == GridPoints.Count;
        }
        public GridPoint GetFirstEmptyGridPoint()
        {
            foreach (var item in GridPoints)
            {
                if (!item.IsFilled())
                    return item;
            }

            return null;
        }
        public GridPoint IsAnyMatchOnGridPoints(int itemIndex)
        {
            for (int i = GridPoints.Count - 1; i >= 0; i--)
            {
                GridPoint item = GridPoints[i];

                if (item.GetFilledItemIndex() == itemIndex)
                {
                    return item;
                }
            }

            return null;
        }
        public void SlideGridPointsToEnd(int index)
        {
            for (int i = GridPoints.Count - 1; i >= index; i--)
            {
                GridPoints[i].FillGrid(GridPoints[i - 1].GetFilledItemIndex());

                if (GridPoints[i].IsFilled())
                    GridPoints[i].ChangeActivationImage(true);
                else
                    GridPoints[i].ChangeActivationImage(false);
            }
        }
        public void DeleteMatchedItems()
        {
            List<GridPoint> matchedGridPoints = new List<GridPoint>();

            int counter;
            for (int i = 0; i < GridPoints.Count; i++)
            {
                counter = 0;
                int controlIndex = GridPoints[i].GetFilledItemIndex();
                for (int j = 0; j < GridPoints.Count; j++)
                {
                    if (controlIndex == GridPoints[j].GetFilledItemIndex() && GridPoints[j].IsFilled())
                    {
                        counter++;
                    }
                }

                if (counter == 3)
                {
                    matchedGridPoints = GridPoints.FindAll(item => item.GetFilledItemIndex() == controlIndex && item.HasItemReached);
                    if (matchedGridPoints.Count == 3)
                    {
                        foreach (var item in matchedGridPoints)
                        {
                            CheckDestroyedItemsWithTargets(item.GetFilledItemIndex());
                            item.UnFillGrid();

                            Debug.Log("Can spawn Particle");
                        }
                        GameEventsManager.Instance.EvntMergeComplete();
                    }
                }
            }
        }
        private void ReArange()
        {
            List<Transform> ReArangedItems = new List<Transform>();
            List<GridPoint> FilledGrids = new List<GridPoint>();
            foreach (GridPoint item in GridPoints)
            {
                if(item.IsFilled() == true)
                {
                    FilledGrids.Add(item);
                }
            }
    
            foreach (GridPoint item in FilledGrids)
            {
                Transform createdItem = Instantiate(GameDataContainer.Instance.ItemInfo[item.GetFilledItemIndex()].Item, null).transform;
                ReArangedItems.Add(createdItem);
            }

            Controller.PlayerController playerController = FindObjectOfType<Controller.PlayerController>();
            playerController.ReArange(ReArangedItems);
        }
        private void CheckDestroyedItemsWithTargets(int itemIndex)
        {
            CardAreaController cardArea = CanvasManager.Instance.GetActiveCanvas().GetComponent<GamePlayCanvasUI>().CardAreaController;
            cardArea.CheckTargetCards(itemIndex);
        }
    }
}