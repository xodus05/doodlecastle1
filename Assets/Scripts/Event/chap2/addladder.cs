using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addladder : MonoBehaviour
{
    public Dialogue dialogue_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerMove thePlayer;
    private NumberSystem theNumber;
    private Inventory inventory;
    private Camshake theCam;

    private static bool flag;
    private static bool flag2;
    private static bool isOpen;
    public int correctNumber;
    public GameObject Panel;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        inventory = FindObjectOfType<Inventory>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theNumber = FindObjectOfType<NumberSystem>();
        theCam = FindObjectOfType<Camshake>();
        if (inventory.haveItem("사다리")) Panel.SetActive(false);
    }

    // Update is called once per frame
/*    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (!flag && Input.GetKey(KeyCode.Z) && thePlayer.animator.GetFloat("DirY") == 1f)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }*/
    void Update()
    {
        if (isOpen) Panel.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Z) && !flag && thePlayer.animator.GetFloat("DirY") == 1f && flag2)
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
        inventory.inventoryItemList.Add(new Item(5005, "사다리", Item.ItemType.Use));
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);
        Panel.SetActive(false);
        Camera.main.GetComponent<Camshake>().Shake();
        theOrder.Move();

    }
}
