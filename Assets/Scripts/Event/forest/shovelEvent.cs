        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shovelEvent : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    public Choice choice_1;

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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag && Input.GetKey(KeyCode.Z) && thePlayer.animator.GetFloat("DirY") == 1f)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.NotMove();
        if(thePlayer.haveShovel) {
            theChoice.ShowChoice(choice_1);
            yield return new WaitUntil(() => !theChoice.choiceIng);
            switch(theChoice.GetResult()) {
            case 0 : 
                theDM.ShowDialogue(dialogue_2);
                yield return new WaitUntil(()=>!theDM.talking);
            break;
            case 1 :
                flag = false;
            break;
            }
        }
        else {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(()=>!theDM.talking);
            flag = false;
        }
        theOrder.Move();
    }
}
