using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCastle : MonoBehaviour
{
    [SerializeField]
    public Dialogue Dialogue_1;
    public Dialogue Dialogue_2;
    public Dialogue Dialogue_3;
    public Choice choice_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private PlayerMove thePlayer;   // DirY == 1f

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theChoice = FindObjectOfType<ChoiceManager>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기

        theOrder.NotMove();

        theDM.ShowDialogue(Dialogue_1);

        yield return new WaitUntil(()=>!theDM.talking);

        theOrder.Move("player", "UP");
        theOrder.Move("player", "UP");
        theOrder.Move("player", "UP");
        theOrder.Move("player", "UP");
        theOrder.Move("player", "UP");

        yield return new WaitUntil(()=>thePlayer.queue.Count == 0);

        theDM.ShowDialogue(Dialogue_2);

        yield return new WaitUntil(()=>!theDM.talking);

        theChoice.ShowChoice(choice_1);
        yield return new WaitUntil(() => !theChoice.choiceIng);
        Debug.Log(theChoice.GetResult());
        switch(theChoice.GetResult()) {
            case 0 : 
                Dialogue_3.sentences[0] = "거절은 받지 않겠네. 지금 당장 전송해주지!";
                break;
            case 1 : 
                Dialogue_3.sentences[0] = "오 정말 고맙네! 지금 당장 전송해주지!";
                break;

        }
        theDM.ShowDialogue(Dialogue_3);
        theOrder.Move();
    }
}
