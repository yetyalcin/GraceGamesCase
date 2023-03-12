using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Game.Core;

[System.Serializable]
public class ItemInfo
{
    public int ID;
    public Item3D Item;
    public Sprite ItemSprite;
}

public static class GlobalVariables
{
    public static int ItemLayerIndex = 6;

    public enum GameStates : byte
    {
        Start,
        Run,
        End
    }
    public enum BoosterType : byte
    {
        Freeze,
        Electric,
        Magnet,
        Clock
    }


}