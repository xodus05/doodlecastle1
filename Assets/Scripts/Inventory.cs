using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private OrderManager theOrder;
    private PlayerMove thePlayer;

    private InventorySlot[] slots; // �κ��丮 ���Ե�;

    public List<Item> inventoryItemList; // �÷��̾ ������ ������ ����Ʈ;
    public List<Item> inventoryTabList; // ������ �ǿ� ���� �ٸ��� ������ ������ ����Ʈ;
    public List<string> activeList;

    public Transform tf; // �θ�ü Slot;

    public GameObject go; // �κ��丮 Ȱ��ȭ ��Ȱ��ȭ;
    public GameObject[] selectedTabImages; // �� �г�

    private int selectedItem; // ���õ� ������;
    private int selectedTab; // ���õ� ��;

    private bool activated; // �κ��丮 Ȱ��ȭ�� true;
    private bool tabActivated; // �� Ȱ��ȭ�� true;
    private bool itemActivated; // ������ Ȱ��ȭ�� true;
    private bool stopKeyInput; // Ű �Է� ���� (�Һ��� �� ���ǰ� ���� �ٵ�, �� �� Ű �Է� ����);
    private bool preventExec; // �ߺ����� ����;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        activeList = new List<string>();
        slots = tf.GetComponentsInChildren<InventorySlot>();
        
    }

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    } // �� Ȱ��ȭ
    public void RemoveSlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    } // �κ��丮 ���� �ʱ�ȭ

    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for (int i = 0; i < selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color;
        }
        StartCoroutine(SelectedTabEffectCoroutine());
    }

    public bool haveItem(string a) {    // 해당 아이템을 가지고 있는지
        foreach (Item i in inventoryItemList)
        {
            if(string.Compare(a, i.itemName) == 0) return true;
        }
        return false;
    }

    public bool doing(string a) {   // 해당 행동을 수행할 조건을 만족했는지
        foreach (string i in activeList)
        {
            if(string.Compare(a, i) == 0) return true;
        }
        return false;
    }

    public void deleteItem(string a) {
        int c = 0;
        foreach (Item i in inventoryItemList)
        {
            if(string.Compare(a, i.itemName) == 0) inventoryItemList.RemoveAt(c);
            c++;
        }
    }

    public void deleteActive(string a) {
        int c = 0;
        foreach (string i in activeList)
        {
            if(string.Compare(a, i) == 0) activeList.RemoveAt(c);
            c++;
        }
    }

    IEnumerator SelectedTabEffectCoroutine()
    {
        while (tabActivated)
        {
            Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
            while (color.a < 0.5f)
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
    } // ���õ� �� ��¦�� ȿ��

    public void ShowItem()
    {
        inventoryTabList.Clear(); // �κ��丮 �� ����Ʈ �ʱ�ȭ
        RemoveSlot(); // ���� ����
        selectedItem = 0;

        switch (selectedTab)
        {
            case 0:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if(Item.ItemType.Use == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]); // �������� ������ ��
                }
            break;
            /*case 1:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Use == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]); // �������� ������ ��
                }
                break;*/
        } //�ǿ� ���� ������ �з�. �װ��� �κ��丮 �� ����Ʈ�� �߰�

        for(int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Additem(inventoryTabList[i]);
        } // �κ��丮 �� ����Ʈ�� ������, �κ��丮 ���Կ� �߰�

        SelectedItem();
    } // ������ Ȱ��ȭ (inventoryTabList�� ���ǿ� �´� �����۵鸸 �־��ְ�, �κ��丮 ���� ���)

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

    } // ���õ� ������ ����, �ٸ� ��� ���� �÷� ���İ� 0
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

            yield return new WaitForSeconds(0.3f);

        }
    } // ���õ� ������ ��¦�� ȿ��

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
                    //selectedTab = 0;
                    tabActivated = true; // �Ǻ��� ������� �ֵ���
                    itemActivated = false;
                    ShowTab();
                }
                else
                {
                    go.SetActive(false);
                    tabActivated = false;
                    itemActivated = false;
                    theOrder.Move();
                    preventExec = true;
                    StopAllCoroutines();
                }
            }

            if (activated)
            {
                if (tabActivated)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow)) // ȭ��ǥ ������ �̵�
                    {
                        if (selectedTab < selectedTabImages.Length - 1)
                            selectedTab++;
                        else
                            selectedTab = 0;
                        //SelectedTab();
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
                        itemActivated = true; // ������ â
                        tabActivated = false; // �� â
                        preventExec = true; // �ߺ�Ű ����
                        ShowItem();
                    }
                }

                if (itemActivated)
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if (selectedItem < inventoryTabList.Count - 2)
                            selectedItem += 2; // �Ʒ��� 2�� ������ 0, 2, 4, 6
                        else
                            selectedItem %= 2; // �ٽ� 0
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
                        if (selectedItem < inventoryTabList.Count - 1)
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
                    if (selectedTab == 0)
                    {
                        stopKeyInput = true;
                        // ������ ��� �Ұų� �ϴ� ���� ������
                    }
                    else if (selectedTab == 1)
                    {
                        // ����. ��� �� �߳�
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

                if (Input.GetKeyUp(KeyCode.Z)) // �ߺ����� ����
                    preventExec = false;
            }
        }
    }
}
