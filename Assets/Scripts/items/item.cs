using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Item/Create New Item")]
public class item : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite icon;
    public int value;
    public ItemType itemType;

    public ItemType getType()
    {
        return itemType;
    }

   public enum ItemType
    {
        Potion,
        Plank,
        Barrel,
        Crate
    }
}
