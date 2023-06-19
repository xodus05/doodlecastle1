using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addKing : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerMove thePlayer;
    private NumberSystem theNumber;
    private Inventory inventory;

    private static bool flag;
    private static bool flag2;
    private static bool isOpen;
    public GameObject Panel;
    public GameObject Panel1;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        inventory = FindObjectOfType<Inventory>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theNumber = FindObjectOfType<NumberSystem>();
        if (inventory.haveItem("왕관")) Panel1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen) Panel1.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Z) && !flag && this.gameObject.ToString() == thePlayer.scanObject.ToString())
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

        if (inventory.haveItem("상자열쇠"))
        {
            theOrder.NotMove();
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);
            Panel1.SetActive(true);
            inventory.inventoryItemList.Add(new Item(5007, "왕관", Item.ItemType.Use));
            yield return new WaitForSeconds(0.5f);
            theDM.ShowDialogue(dialogue_2);
            yield return new WaitUntil(() => !theDM.talking);
            isOpen = true;
        }
        flag = true;
        theOrder.Move();
    }
}
