using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private OrderManager theOrder;
    private PlayerMove thePlayer;

    private InventorySlot[] slots; // 인벤토리 슬롯들;

    private List<Item> inventoryItemList; // 플레이어가 소지한 아이템 리스트;
    private List<Item> inventoryTabList; // 선택한 탭에 따라 다르게 보여질 아이템 리스트;

    public Transform tf; // 부모객체 Slot;

    public GameObject go; // 인벤토리 활성화 비활성화;
    public GameObject[] selectedTabImages; // 탭 패널

    private int selectedItem; // 선택된 아이템;
    private int selectedTab; // 선택된 탭;

    private bool activated; // 인벤토리 활성화시 true;
    private bool tabActivated; // 탭 활성화시 true;
    private bool itemActivated; // 아이템 활성화시 true;
    private bool stopKeyInput; // 키 입력 제한 (소비할 때 질의가 나올 텐데, 그 때 키 입력 방지);
    private bool preventExec; // 중복실행 제한;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>();
        inventoryItemList.Add(new Item(thePlayer.haveShovel, Item.ItemType.Use));
    }

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    } // 탭 활성화
    public void RemoveSlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    } // 인벤토리 슬롯 초기화

    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for(int i = 0; i < selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color;
        }
        StartCoroutine(SelectedTabEffectCoroutine());
    } // 선택된 탭 제외하고 다른 모든 탭의 컬러 알파값 0

    IEnumerator SelectedTabEffectCoroutine()
    {
        while (tabActivated)
        {
            Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
            while(color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }

        }
    } // 선택된 탭 반짝임 효과

    public void ShowItem()
    {
        inventoryTabList.Clear(); // 인벤토리 탭 리스트 초기화
        RemoveSlot(); // 슬롯 제거
        selectedItem = 0;

        switch (selectedTab)
        {
            case 0:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if(Item.ItemType.Use == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]); // 아이템이 탭으로 들어감
                }
            break;
/*            case 1:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Use == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]); // 아이템이 탭으로 들어감
                }
                break;*/
        } //탭에 따른 아이템 분류. 그것을 인벤토리 탭 리스트에 추가

        for(int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Additem(inventoryTabList[i]);
        } // 인벤토리 탭 리스트의 내용을, 인벤토리 슬롯에 추가

        SelectedItem();
    } // 아이템 활성화 (inventoryTabList에 조건에 맞는 아이템들만 넣어주고, 인벤토리 슬롯 출력)

    public void SelectedItem()
    {
        StopAllCoroutines();
        if(inventoryTabList.Count > 0)
        {
            Color color = slots[0].selected_Item.GetComponent<Image>().color;
            color.a = 0f;
            for(int i = 0; i < inventoryTabList.Count; i++)
            {
                slots[i].selected_Item.GetComponent<Image>().color = color;
            }
            StartCoroutine(SelectedItemEffectCoroutine());
        }
        else
        {

        }

    } // 선택된 아이템 제외, 다른 모든 탭의 컬러 알파값 0
    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }

        }
    } // 선택된 아이템 반짝임 효과

    // Update is called once per frame
    void Update()
    {
        if(!stopKeyInput)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                activated = !activated;

                if (activated)
                {
                    theOrder.NotMove();
                    go.SetActive(true);
                    selectedTab = 0;
                    tabActivated = true; // 탭부터 띄워질수 있도록
                    itemActivated = false;
                    ShowTab();
                }
                else
                {
                    StopAllCoroutines();
                    go.SetActive(false);
                    tabActivated = false;
                    itemActivated = false;
                    theOrder.Move();
                }
            }

            if (activated)
            {
                if (tabActivated)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow)) // 화살표 누를시 이동
                    {
                        if (selectedTab < selectedTabImages.Length - 1)
                            selectedTab++;
                        else
                            selectedTab = 0;
                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedTab > 0)
                            selectedTab--;
                        else
                            selectedTab = selectedTabImages.Length - 1;
                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.Z))
                    {
                        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
                        color.a = 0.25f;
                        selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                        itemActivated = true; // 아이템 창
                        tabActivated = false; // 탭 창
                        preventExec = true; // 중복키 방지
                        ShowItem();
                    }
                }

                else if (itemActivated)
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if (selectedItem < inventoryTabList.Count - 2)
                            selectedItem += 2; // 아래로 2씩 내려감 0, 2, 4, 6
                        else
                            selectedItem %= 2; // 다시 0
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (selectedItem > 1)
                            selectedItem -= 2; 
                        else
                            selectedItem = inventoryTabList.Count - 2 - selectedItem;
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedItem < inventoryTabList.Count - 2)
                            selectedItem++;
                        else
                            selectedItem = 0;
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedItem < 0)
                            selectedItem--;
                        else
                            selectedItem = inventoryTabList.Count - 2;
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.Z) && !preventExec)
                    {
                        if(selectedTab == 0)
                        {
                            stopKeyInput = true;
                            // 아이템 사용 할거냐 하는 질의 선택지
                        }
                        else if(selectedTab == 1)
                        {
                            // 저장. 등등 뭐 했냐
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.X))
                    {
                        StopAllCoroutines();
                        itemActivated = false;
                        tabActivated = true;
                        ShowTab();
                    }
                }

                if (Input.GetKeyUp(KeyCode.Z)) // 중복실행 방지
                    preventExec = false;
            }
        }
    }
}
