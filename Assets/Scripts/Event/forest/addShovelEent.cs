using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addShovelEent : MonoBehaviour
{

    private Inventory inventory;

    public GameObject Panel;
    public Dialogue dialogue_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private PlayerMove thePlayer;

    private static bool flag;

    void Start()
    {
        theChoice = FindObjectOfType<ChoiceManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        inventory = FindObjectOfType<Inventory>();
        inventory.inventoryItemList.Add(new Item(thePlayer.haveShovel, 5001, "»ð", Item.ItemType.Use));
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
        thePlayer.haveShovel = true;

        Panel.SetActive(false);
        theOrder.Move();
    }

}
