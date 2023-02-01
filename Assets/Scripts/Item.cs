using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{

    public enum ItemType {
        Quest,
        Use
    }

    public int itemID;  // 아이템의 고유 ID값, 중복 불가능. (50001, 50002)
    public string itemName; // 아이템의 이름
    public string itemDescription;  // 아이템 설명
    public int itemCount;    // 소지 개수
    public Sprite itemIcon;
    public ItemType itemType;

    public Item(bool haveShovel, ItemType use)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
