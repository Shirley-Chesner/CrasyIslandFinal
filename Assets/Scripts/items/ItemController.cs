using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    item currentItem;

    void removeItem()
    {
        playerInventory.instance.remove(currentItem);
        Destroy(gameObject);
    }

    public void addItem(item newItem)
    {
        currentItem = newItem;
    }

    public void useItem()
    {
        switch (currentItem.itemType)
        {
            case item.ItemType.Potion:
                playerInventory.instance.usePotion(currentItem.value);
                break;
            case item.ItemType.Plank:
                break;
        }
        removeItem();
    }
}
