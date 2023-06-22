using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class leafEvent2 : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public GameObject Panel;
    public Choice choice_1;
    public Choice choice_2;
    private PlayerMove thePlayer;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private Inventory inventory;
    private ChoiceManager theChoice;

    BoxCollider2D boxCollider;

    private bool flag;
    private bool flag2;
    public bool isActive; //체크 확인



    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = FindObjectOfType<Inventory>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theChoice = FindObjectOfType<ChoiceManager>();
        //if (inventory.haveItem("도서관 열쇠")) Panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !flag && thePlayer.scanObject && this.gameObject.ToString() == thePlayer.scanObject.ToString())
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

        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        if (inventory.doing("버튼"))
        {
            theChoice.ShowChoice(choice_1);
            yield return new WaitUntil(() => !theChoice.choiceIng);
            Debug.Log(theChoice.GetResult());
            switch (theChoice.GetResult())
            {
                case 0:
                    theChoice.ShowChoice(choice_2);
                    yield return new WaitUntil(() => !theChoice.choiceIng);
                    switch (theChoice.GetResult())
                    {
                        case 0:
                            dialogue_1.sentences[0] = "문이 열리는 소리가 났다.";
                            theDM.ShowDialogue(dialogue_1);
                            yield return new WaitUntil(() => !theDM.talking);
                            dialogue_2.sentences[0] = "풀었다!";
                            theDM.ShowDialogue(dialogue_2);
                            yield return new WaitUntil(() => !theDM.talking);
                            inventory.inventoryItemList.Add(new Item(5004, "도서관 열쇠", Item.ItemType.Use));
                            thePlayer.queue.Clear();
                            isActive = true;
                            break;
                    }
                    flag = false;
                    break;
            }

        }
        else
        {
            dialogue_3.sentences[0] = "이건 뭐지? 누를수 있을 것 같은데..";
            theDM.ShowDialogue(dialogue_3);
            yield return new WaitUntil(() => !theDM.talking);
        }
        flag = false;
        theOrder.Move();
    }

}
