using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bookEvent1 : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    private PlayerMove thePlayer;

    private DialogueManager theDM;
    private OrderManager theOrder;
    BoxCollider2D boxCollider;

    private bool flag;
    private bool flag2;



    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        thePlayer = FindObjectOfType<PlayerMove>();
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
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);
        yield return new WaitForSeconds(0.1f);
        flag = false;
        theOrder.Move();
    }

}
