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

    private static bool flag;
    private static bool isOpen2;
    public int correctNumber;
    public GameObject Panel;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theNumber = FindObjectOfType<NumberSystem>();
        if (isOpen2) Panel.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !flag && thePlayer.scanObject && this.gameObject.ToString()==thePlayer.scanObject.ToString())
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
        theOrder.PreLoadCharacter(); // 리스트 채우기
        theOrder.NotMove();

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theNumber.ShowNumber(correctNumber);
        yield return new WaitUntil(() => !theNumber.activated);
        if (theNumber.GetResult())
        {
            dialogue_2.sentences[0] = "열렸어!\n들어가자.";
            Panel.SetActive(true);
            isOpen2 = true;
        }
        else dialogue_2.sentences[0] = "틀렸어...";
        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);
        flag = false;
        if (isOpen2) flag = true;
        theOrder.Move();
    }
}
