using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class campEvent : MonoBehaviour
{

    private Inventory inventory;

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Choice choice_1;

    private FadeManager theFade;
    private DialogueManager theDM;
    private ChoiceManager theChoice;
    private OrderManager theOrder;

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
            switch (theChoice.GetResult())
            {
                case 0 :
                    // 불타는 거 등장
                    theFade.Flash(1f);
                    yield return new WaitUntil(() => !theDM.talking);
                    break;
               
            }
        }
        else
        {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);
            yield return new WaitForSeconds(0.1f);
            flag = false;
        }
        theOrder.Move();
    }
}