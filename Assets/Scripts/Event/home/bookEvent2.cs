using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookEvent2 : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Choice choice_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private PlayerMove thePlayer;

    public GameObject Panel;
    public GameObject Panel2;
    public GameObject Panel3;

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
        if (!flag && Input.GetKey(KeyCode.Z) && thePlayer.animator.GetFloat("DirY") == 1f)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기
        theOrder.NotMove();

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(()=>!theDM.talking);
        theChoice.ShowChoice(choice_1);
        yield return new WaitUntil(() => !theChoice.choiceIng);
        
        if(theChoice.GetResult()==0) {
            Panel.SetActive(true);
            theDM.ShowDialogue(dialogue_2);
            yield return new WaitUntil(()=>!theDM.talking);
            Panel2.SetActive(true);
            theDM.ShowDialogue(dialogue_3);
            yield return new WaitUntil(()=>!theDM.talking);
            Panel.SetActive(false);
            Panel2.SetActive(false);
            Panel3.SetActive(true);
        }
        else {
            flag = false;
        }

        theOrder.Move();
    }
}
