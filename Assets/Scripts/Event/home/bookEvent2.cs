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
    private FadeManager theFade;

    BoxCollider2D boxCollider;

    public GameObject Panel;
    public GameObject Panel2;
    public GameObject Panel3;

    private bool flag;
    private bool flag2;
    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theFade = FindObjectOfType<FadeManager>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !flag && thePlayer.animator.GetFloat("DirY") == 1f && flag2)
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
        yield return new WaitForSeconds(0.1f);
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(()=>!theDM.talking);
        theOrder.NotMove();
        boxCollider.enabled = false;
        boxCollider.enabled = true;
        theChoice.ShowChoice(choice_1);
        yield return new WaitUntil(() => !theChoice.choiceIng);
        
        if(theChoice.GetResult()==0) {
            Panel.SetActive(true);
            theDM.ShowDialogue(dialogue_2);
            yield return new WaitUntil(()=>!theDM.talking);
            theFade.Flash();
            Panel2.SetActive(true);
            theDM.ShowDialogue(dialogue_3);
            yield return new WaitUntil(()=>!theDM.talking);
            Panel.SetActive(false);
            Panel2.SetActive(false);
            Panel3.SetActive(true);
            theFade.Flash();
        }
        else {
            flag = false;
        }
        theOrder.Move();
    }
}
