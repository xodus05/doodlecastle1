using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private OrderManager theOrder;
    private AudioManager theAudio;
    public string key_sound;
    public string enter_sound;
    public string cancel_sound;
    public string open_sound;
    public string beep_sound;

    private InventorySlot[] slots; // �κ��丮 ���Ե�
    private List<Item> inventoryItemList; // �÷��̾ ������ ������ ����Ʈ.
    public Transform tf; //slot �θ�ü
    public GameObject go; // �κ��丮 Ȱ��ȭ ��Ȱ��ȭ
    public GameObject[] selectedItemImages;

    public int selectedItem; // ���õ� ������.
    public int selectedTab; // ���õ� ��
    private bool activated; // �κ��丮 Ȱ��ȭ�� true;
    //private bool tabActivated; // �� Ȱ��ȭ�� true;
    private bool itemActivated; // ������ Ȱ��ȭ�� true
    private bool stopKeyInput; // key �Է� ����. ������.. ���� ���? �� �̷���..? ���ص��ɵ�
    private bool preventExce; // �ߺ�Ű ����

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    // Start is called before the first frame update
    void Start()
    {
        theOrder = GetComponent<OrderManager>();
        theAudio = FindObjectOfType<AudioManager>();
        inventoryItemList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>(); 
    }

    public void ShowTab() // �κ��丮 �� �����ִ� �Լ�
    {
        RemoveSlot();
        SelectedItem();
    }

    public void RemoveSlot() // ���԰��� InventorySlot���� ó�� �����ִ� �Լ�
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(true); // ���� �����ϱ� �ٷ� ������â true

        }
    }

    public void SelectedItem()
    {
        StopAllCoroutines();
        Color color = selectedItemImages[selectedItem].GetComponent<Image>().color;
        color.a = 0f;
        for(int i = 0; i < selectedItemImages.Length; i++)
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
            while(color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedItemImages[selectedItem].GetComponent <Image>().color = color;
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
        if(!stopKeyInput)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                activated = !activated;

                if(activated)
                {
                    theOrder.NotMove();
                    go.SetActive(true);
                    selectedTab = 0;
                    itemActivated = false;
                    ShowTab();

                }
                else
                {
                    StopAllCoroutines();
                    go.SetActive(false);
                    itemActivated = false;
                    theOrder.Move();
                    ShowTab();
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
                        SelectedItem();
                    }
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedItem > 0)
                        {
                            selectedItem--;
                        }
                        else
                        {
                            selectedItem = selectedItemImages.Length - 1;
                        }
                        SelectedItem();
                    }
                }
            }
        }
    }
}
