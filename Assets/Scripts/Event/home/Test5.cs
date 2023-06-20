using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test5 : MonoBehaviour
{

    public Dialogue dialogue_1;
    //public Dialogue dialogue_2;
    public GameObject Panel;
    public Choice choice_1;
    private PlayerMove thePlayer;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private Inventory inventory;
    private ChoiceManager theChoice;

    BoxCollider2D boxCollider;

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = FindObjectOfType<Inventory>();
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
        yield return new WaitForSeconds(0.1f);
        theOrder.NotMove();

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theChoice.ShowChoice(choice_1);
        yield return new WaitUntil(() => !theChoice.choiceIng);
        Debug.Log(theChoice.GetResult());
        switch (theChoice.GetResult())
        {
            case 0:
                SceneManager.LoadScene("start");
                thePlayer.transform.position = new Vector2(-10728, 1832);
                break;
            case 1:
                SceneManager.LoadScene("castle");
                thePlayer.transform.position = new Vector2(-6239, -240);
                thePlayer.isBoss = true;
                break;
            default:
                break;
        }

        flag = false;

        theOrder.Move();
    }

}
