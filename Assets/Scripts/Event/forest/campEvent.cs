using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class campEvent : MonoBehaviour
{

    private Inventory inventory;

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Choice choice_1;

    private FadeManager theFade;
    private DialogueManager theDM;
    private ChoiceManager theChoice;
    private OrderManager theOrder;

    public GameObject Panel;
    public GameObject Panel2;

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theOrder = FindObjectOfType<OrderManager>();
        inventory = FindObjectOfType<Inventory>();
        theFade = FindObjectOfType<FadeManager>();
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
        if (inventory.haveItem("라이터"))
        {
            theDM.ShowDialogue(dialogue_2);
            yield return new WaitUntil(() => !theDM.talking);
            theChoice.ShowChoice(choice_1);
            yield return new WaitUntil(() => !theChoice.choiceIng);
            Debug.Log(theChoice.GetResult());
            switch(theChoice.GetResult()) {
                case 0 :
                    theFade.Flash(0.01f);
                    yield return new WaitForSeconds(1.0f);
                    Panel.SetActive(true);
                    Panel2.SetActive(true);
                    yield return new WaitForSeconds(1.5f);
                    theDM.ShowDialogue(dialogue_3);
                    yield return new WaitUntil(() => !theDM.talking);
                    theFade.Flash(0.01f);
                    break;
                case 1 :
                    flag = false;
                    break;
            }
        }
        else
        {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);
            inventory.inventoryItemList.Add(new Item(5003, "라이터", Item.ItemType.Use));
            flag = false;
            yield return new WaitForSeconds(0.1f);
        }
        theOrder.Move();
    }
}