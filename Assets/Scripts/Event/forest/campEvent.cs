using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class campEvent : MonoBehaviour
{

    private Inventory inventory;

    public string transferMapName; //이동할 맵의 이름
    public int startPointNumber;

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Choice choice_1;

    private FadeManager theFade;
    private DialogueManager theDM;
    private ChoiceManager theChoice;
    private OrderManager theOrder;
    private PlayerMove thePlayer;
    private Enemy theEnemy;

    public GameObject Panel;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;
    public GameObject Panel5;

    private bool flag;
    private bool flag2;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theOrder = FindObjectOfType<OrderManager>();
        inventory = FindObjectOfType<Inventory>();
        theFade = FindObjectOfType<FadeManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theEnemy = FindObjectOfType<Enemy>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !flag && flag2)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        flag2 = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        flag2 = false;
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
            yield return new WaitUntil(() => !theChoice.choiceIng);
            Debug.Log(theChoice.GetResult());
            switch(theChoice.GetResult()) {
                case 0 :
                    inventory.activeList.Add("불");  // 불이 나는 상황 추가
                    theFade.Flash(0.01f);   // 불이 나면서 플래시 이벤트
                    yield return new WaitForSeconds(1.0f);
                    Panel.SetActive(true);  // 불 나타나기
                    Panel2.SetActive(true); // 빨간 배경 나타나기
                    theEnemy.monster.SetActive(false);  // 몬스터 움직임 멈춤
                    yield return new WaitForSeconds(1.5f);
                    theDM.ShowDialogue(dialogue_3);
                    yield return new WaitUntil(() => !theDM.talking);
                    theFade.Flash(0.01f);
                    yield return new WaitForSeconds(1.0f);
                    Panel3.SetActive(true);
                    yield return new WaitForSeconds(5.0f);
                    Panel4.SetActive(true);
                    yield return new WaitForSeconds(5.0f);
                    Panel5.SetActive(true);
                    yield return new WaitForSeconds(5.0f);
                    theFade.FadeOut();
                    yield return new WaitForSeconds(0.1f);
                    Panel.SetActive(false);
                    Panel2.SetActive(false);
                    Panel3.SetActive(false);
                    Panel4.SetActive(false);
                    Panel5.SetActive(false);
                    thePlayer.startPointNumber = startPointNumber;
                    thePlayer.currentMapName = transferMapName;
                    SceneManager.LoadScene(transferMapName); // 이동할 맵의 이름으로 이동
                    theFade.FadeIn();
                    break;
                case 1 :
                    flag = false;
                    break;
            }
        }
        else
        {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);
            // inventory.inventoryItemList.Add(new Item(5003, "라이터", Item.ItemType.Use));
            flag = false;
            yield return new WaitForSeconds(0.1f);
        }
        theOrder.Move();
    }
}