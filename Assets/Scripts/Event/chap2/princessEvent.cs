using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class princessEvent : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public GameObject Panel;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private Inventory inventory;
    private controlEvent control;
    private PlayerMove thePlayer;

    BoxCollider2D boxCollider;

    private bool flag;
    private bool flag2;
    private bool isFirst;


    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theOrder = FindObjectOfType<OrderManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = FindObjectOfType<Inventory>();
        control = FindObjectOfType<controlEvent>();
        isFirst = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !flag && this.gameObject.ToString() == thePlayer.scanObject.ToString())
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }


    IEnumerator EventCoroutine()
    {
        theOrder.NotMove();
        yield return new WaitForSeconds(0.1f);
        if (isFirst) theDM.ShowDialogue(dialogue_1);
        else theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);
        flag = false;
        yield return new WaitForSeconds(0.1f);
        theOrder.Move();
    }

}
