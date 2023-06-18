using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fgevent : MonoBehaviour
{

    public Dialogue dialogue_1;
    public GameObject Panel;

    private DialogueManager theDM;
    private PlayerMove thePlayer;
    private OrderManager theOrder;
    private Inventory inventory;
    private controlEvent control;

    BoxCollider2D boxCollider;

    private bool flag;
    private bool flag2;


    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = FindObjectOfType<Inventory>();
        control = FindObjectOfType<controlEvent>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !flag && this.gameObject.ToString()==thePlayer.scanObject.ToString() && thePlayer.touch)
        {
            bool isTrue = this.gameObject.ToString()==thePlayer.scanObject.ToString();
            Debug.Log(this.gameObject.ToString());
            Debug.Log(thePlayer.scanObject.ToString());
            Debug.Log(isTrue);
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
        flag = false;
        theOrder.Move();        
    }

}
