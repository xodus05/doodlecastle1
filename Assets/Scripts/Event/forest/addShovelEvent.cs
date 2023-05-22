using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addShovelEvent : MonoBehaviour
{

    private Inventory inventory;

    public GameObject Panel;
    public Dialogue dialogue_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private ChoiceManager theChoice;

    private static bool flag;
    private static bool flag2;

    void Start()
    {
        theChoice = FindObjectOfType<ChoiceManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        inventory = FindObjectOfType<Inventory>();
        if(inventory.haveItem("삽")) Panel.SetActive(false);
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
        if (collision.gameObject.name == "Player")
            flag2 = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            flag2 = false;
    }


    IEnumerator EventCoroutine()
    {
        theOrder.NotMove();
        yield return new WaitForSeconds(0.1f);
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(()=>!theDM.talking);
        inventory.inventoryItemList.Add(new Item(5001, "삽", Item.ItemType.Use));
        Panel.SetActive(false);
        theOrder.Move();
    }

}
