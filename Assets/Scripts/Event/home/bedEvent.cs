using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bedEvent : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue_1;
    public Choice choice_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private PlayerMove thePlayer;

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theChoice = FindObjectOfType<ChoiceManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag && Input.GetKey(KeyCode.Z) && thePlayer.animator.GetFloat("DirX") == -1f)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기

        theOrder.NotMove();
        //flag = false;
        theChoice.ShowChoice(choice_1);
        yield return new WaitUntil(() => !theChoice.choiceIng);
        if(theChoice.GetResult()==0) {
            
            theOrder.Move("player", "LEFT");
            theOrder.Move("player", "LEFT");
            yield return new WaitForSeconds(3.0f);
            // 부스럭 거리는 효과음 넣기

            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(()=>!theDM.talking);
            flag = true;
        }
        
        theOrder.Move();
    }
}
