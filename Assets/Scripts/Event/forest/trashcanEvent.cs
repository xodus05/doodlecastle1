using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashcanEvent : MonoBehaviour
{
    private Inventory inventory;

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private DialogueManager theDM;
    private OrderManager theOrder;

    BoxCollider2D boxCollider;

    private bool flag;
    private bool flag2;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !flag && flag2)
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
        if(inventory.doing("라이터")) {
            theDM.ShowDialogue(dialogue_2);
            inventory.inventoryItemList.Add(new Item(5003, "라이터", Item.ItemType.Use));
            yield return new WaitUntil(()=>!theDM.talking);
        }
        else {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(()=>!theDM.talking);
            yield return new WaitForSeconds(0.1f);
            flag = false;
        }
        theOrder.Move();
    }
}
