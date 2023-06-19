using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candleEvent : MonoBehaviour
{
    private Inventory inventory;

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;
    public Choice choice_1;
    public Choice choice_2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private PlayerMove thePlayer;

    private static bool flag;
    private static bool flag2;

    // Start is called before the first frame update
    void Start()
    {
        theChoice = FindObjectOfType<ChoiceManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !flag && thePlayer.scanObject && thePlayer.animator.GetFloat("DirY") == 1f && flag2)
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
        theOrder.NotMove();
        theChoice.ShowChoice(choice_1);
        yield return new WaitUntil(() => !theChoice.choiceIng);
        switch(theChoice.GetResult()) {
            case 0 : 
                dialogue_2.sentences[0] = "버젓이 불이 피어오르고 있는데... 저길 어떻게 만져? 진심이냐?";
                theDM.ShowDialogue(dialogue_2);
                yield return new WaitUntil(()=>!theDM.talking);
                flag = false;
            break;
            case 1 : 
                dialogue_2.sentences[0] = "이 아래에는 아무것도 없어. 꽝이다...";
                theDM.ShowDialogue(dialogue_2);
                yield return new WaitUntil(()=>!theDM.talking);
                flag = false;
            break;
            case 3 :
                flag = false;
            break;
            case 2 : 
                theOrder.NotMove();
                theChoice.ShowChoice(choice_2);
                yield return new WaitUntil(() => !theChoice.choiceIng);
                switch(theChoice.GetResult()) {
                    case 0 : 
                    case 2 :
                        theDM.ShowDialogue(dialogue_4);
                        yield return new WaitUntil(()=>!theDM.talking);
                        flag = false;
                    break;
                    case 1 :
                        inventory.inventoryItemList.Add(new Item(5002, "열쇠", Item.ItemType.Use));
                        theDM.ShowDialogue(dialogue_3);
                        yield return new WaitUntil(()=>!theDM.talking);
                    break;
                }
            break;
        }
        theOrder.Move();
    }
}
