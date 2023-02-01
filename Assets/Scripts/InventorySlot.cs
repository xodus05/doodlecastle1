using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text itemName_Text;
    public GameObject selected_Item;

/*    public void Additem(Item _item)
    {
        itemName_Text.text = _item.itemName;
        icon.sprite = _item.itemIcon;
        
    }*/

    public void RemoveItem()
    {
        itemName_Text.text = "";
        icon.sprite = null;
    }
}
