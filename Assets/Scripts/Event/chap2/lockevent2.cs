using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockevent2 : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerMove thePlayer;
    private NumberSystem theNumber;
    private Inventory inventory;

    private static bool flag;
    private static bool flag2;
    public bool isOpen;
    public int correctNumber;
    public GameObject Panel;
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        inventory = FindObjectOfType<Inventory>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theNumber = FindObjectOfType<NumberSystem>();
        if (inventory.haveItem("사다리")) Panel1.SetActive(true);
    }

    // Update is called once per frame

    void Update()
    {
        //if (isOpen) Panel.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Z) && !flag && thePlayer.animator.GetFloat("DirY") == 1f && this.gameObject.ToString() == thePlayer.scanObject.ToString())
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }


    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기
        theOrder.NotMove();
        yield return new WaitForSeconds(0.1f);
        dialogue_1.sentences[0] = "자물쇠가 걸려있다.";
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);
        theNumber.ShowNumber(correctNumber);
        yield return new WaitUntil(() => !theNumber.activated);
        if (theNumber.GetResult())
        {
            theOrder.NotMove();
            Debug.Log(thePlayer.notMove);

            dialogue_3.sentences[0] = "열렸다!!";
            theDM.ShowDialogue(dialogue_3);
            yield return new WaitUntil(() => !theDM.talking);
            Panel1.SetActive(true);
            Panel2.SetActive(true);
            Panel3.SetActive(true);
            // Panel.SetActive(true);
            isOpen = true;
        }
        else
        {
            dialogue_2.sentences[0] = "틀렸어...";
            theDM.ShowDialogue(dialogue_2);
            yield return new WaitUntil(() => !theDM.talking);
            flag = false;

        }
        theOrder.Move();
    }
}
