using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockEvent : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerMove thePlayer;
    private NumberSystem theNumber;

    private bool flag;
    public int correctNumber;
    public GameObject Panel;
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theNumber = FindObjectOfType<NumberSystem>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(isOpen) {
            Panel.SetActive(true);
        }

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

        theNumber.ShowNumber(correctNumber);
        yield return new WaitUntil(() => !theNumber.activated);
        if(theNumber.GetResult()) {
            dialogue_2.sentences[0] = "열렸어!\n들어가자.";
            Panel.SetActive(true);
            isOpen = true;
        }
        else {
            dialogue_2.sentences[0] = "틀렸어...";
            flag = false;
        }
        theDM.ShowDialogue(dialogue_2);
        theOrder.Move();
    }
}
