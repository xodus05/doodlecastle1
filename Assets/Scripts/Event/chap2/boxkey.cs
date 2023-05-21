using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boxkey : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public GameObject Panel;
    public GameObject Panel1;
    public Choice choice_1;
    private PlayerMove thePlayer;
    private static bool isOpen;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private Inventory inventory;
    private string panelKey = "PanelState"; // PlayerPrefs에 저장할 키값

    BoxCollider2D boxCollider;

    private bool flag;
    private bool flag2;



    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        thePlayer = FindObjectOfType<PlayerMove>();
        inventory = FindObjectOfType<Inventory>();
        theChoice = FindObjectOfType<ChoiceManager>();
        //inventory.inventoryItemList.Add(new Item(5005, "사다리", Item.ItemType.Use));
        inventory.inventoryItemList.Add(new Item(5007, "왕관", Item.ItemType.Use));
        if (inventory.haveItem("사다리"))
        {
            Panel.SetActive(true);
            Panel1.SetActive(isOpen);
        }
    }

    void Update()
    {
        if (!flag && Input.GetKeyDown(KeyCode.Z) && flag2)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        flag2 = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        flag2 = false;
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기
        theOrder.NotMove();
        //yield return new WaitUntil(() => thePlayer.queue.Count == 0);
        if (inventory.haveItem("사다리") && inventory.haveItem("상자열쇠") == false)
        {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);

            theChoice.ShowChoice(choice_1);
            yield return new WaitUntil(() => !theChoice.choiceIng);
            switch (theChoice.GetResult())
            {
                case 0:
                    if(inventory.haveItem("상자열쇠") == false)
                    {
                        Panel1.SetActive(true);
                        theDM.ShowDialogue(dialogue_2);
                        yield return new WaitUntil(() => !theDM.talking);
                        inventory.inventoryItemList.Add(new Item(5006, "상자열쇠", Item.ItemType.Use));
                        Panel.SetActive(false);
                        isOpen = false;
                    }
                    break;
                case 1:
                    flag = false;
                    break;
      
            }
        }
        isOpen = true;
        flag = false;
        theOrder.Move();
    }
}
