using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class leafEvent : MonoBehaviour
{

    public Dialogue dialogue_1;
    public GameObject Panel;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private Inventory inventory;

    BoxCollider2D boxCollider;

    private bool flag;



    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = FindObjectOfType<Inventory>();
        if (inventory.haveItem("도서관 열쇠")) Panel.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
            if (!flag && Input.GetKey(KeyCode.Z))
            {
                flag = true;
                StartCoroutine(EventCoroutine());
            }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.NotMove();
        yield return new WaitForSeconds(0.1f);
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(()=>!theDM.talking);
        inventory.inventoryItemList.Add(new Item(5004, "도서관 열쇠", Item.ItemType.Use));
        yield return new WaitForSeconds(0.1f);
        flag = false;
        theOrder.Move();
    }

}
