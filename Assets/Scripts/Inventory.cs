using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private InventorySlot[] slots; // 인벤토리 슬롯들;

    private List<Item> inventoryItemList; // 플레이어가 소지한 아이템 리스트;
    private List<Item> inventoryTabList; // 선택한 탭에 따라 다르게 보여질 아이템 리스트;

    public Transform tf; // 부모객체 Slot;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
