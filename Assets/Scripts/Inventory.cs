using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private OrderManager theOrder;
    public PlayerMove thePlayer;
    //private AudioManager theAudio;
    //public string key_sound;
    //public string enter_sound;
    //public string cancel_sound;
    //public string open_sound;
    //public string beep_sound;

    private InventorySlot[] slots; // 인벤토리 슬롯들
    public List<Item> inventoryItemList; // 플레이어가 소지한 아이템 리스트.
    public List<Item> inventoryTabList;
    public Transform tf; //Panel 부모객체
    public GameObject go; // 인벤토리 활성화 비활성화
    public GameObject[] selectedItemImages;

    public int selectedItem; // 선택된 아이템.
    //public int selectedTab; // 선택된 탭

    private bool activated; // 인벤토리 활성화시 true;
    //private bool tabActivated; // 탭 활성화시 true;
    private bool itemActivated; // 아이템 활성화시 true
    private bool stopKeyInput; // key 입력 제한. 아이템.. 정말 사용? 머 이런거..? 안해도될듯
    private bool preventExce; // 중복키 제한

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    // Start is called before the first frame update
    void Start()
    {
        theOrder = GetComponent<OrderManager>();
        //theAudio = FindObjectOfType<AudioManager>();
        inventoryItemList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>();
        //inventoryItemList.Add(new Item(thePlayer.haveShovel = true, Item.ItemType.Use));

    }

    public void ShowItem() // 인벤토리 탭 보여주는 함수
    {
        RemoveSlot();
        SelectedItem();
    }

    public void RemoveSlot() // 슬롯값을 InventorySlot에서 처럼 없애주는 함수
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false); // 탭이 없으니까 바로 아이템창 true

        }
    }

    public void SelectedItem()
    {
        StopAllCoroutines();
        Color color = selectedItemImages[selectedItem].GetComponent<Image>().color;
        color.a = 0f;
        for (int i = 0; i < selectedItemImages.Length; i++)
        {
            selectedItemImages[i].GetComponent<Image>().color = color;
        }
        StartCoroutine(SelectedItemEffectCoroutine());
    }

    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated)
        {
            Color color = selectedItemImages[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedItemImages[selectedItem].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a < 0f)
            {
                color.a += 0.03f;
                selectedItemImages[selectedItem].GetComponent<Image>().color = color;
                yield return waitTime;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!stopKeyInput)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                activated = true;

                if(activated)
                {
                    //theOrder.NotMove();
                    go.SetActive(true);
                    selectedItem = 0;
                    itemActivated = true;
                    ShowItem();
                    
                }
                else
                {
                    StopAllCoroutines();
                    go.SetActive(true);
                    itemActivated = false;
                    theOrder.Move();
                    ShowItem();
                }
            }

            if (activated)
            {
                if (itemActivated)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if(selectedItem < selectedItemImages.Length - 1)
                        {
                            selectedItem++;
                        } else
                        {
                            selectedItem = 0;
                        }
                        //SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedItem > 0)
                        {
                            selectedItem--;
                        }
                        else
                        {
                            selectedItem = selectedItemImages.Length - 1;
                        }
                        //SelectedItem();
                    }
                    else if(Input.GetKeyDown(KeyCode.Q))
                    {
                        Color color = selectedItemImages[selectedItem].GetComponent<Image>().color;
                        color.a = 0.25f;
                        selectedItemImages[selectedItem].GetComponent<Image>().color = color;
                        itemActivated = true;
                        preventExce = true; // 중복실행 방지
                        ShowItem();
                    }
                }
            }
        }
    }
}
