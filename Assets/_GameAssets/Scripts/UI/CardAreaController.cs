using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Game.Managers;

namespace Game.UI
{
    public class CardAreaController : MonoBehaviour
    {
        public List<CardItem> CardItems;


        public void CheckTargetCards(int itemIndex)
        {
            for (int i = 0; i < CardItems.Count; i++)
            {
                if (CardItems[i].GetItemIndex().Equals(itemIndex) && CardItems[i].GetTargetCount() != 0)
                    CardItems[i].SetTargetCount(CardItems[i].GetTargetCount() - 1);

                if (CardItems[i].GetTargetCount() == 0)
                {
                    CollectTarget(CardItems[i]);
                }
            }
        }
        private void CollectTarget(CardItem cardItem)
        {
            cardItem.ComplateCardTarget();
            CardItems.Remove(cardItem);

            if (CardItems.Count.Equals(0))
            {
                CanvasManager.Instance.SetActiveCanvas(2);
                GamePlayManager.Instance.GameState = GlobalVariables.GameStates.End;
            }
        }
    }
}

