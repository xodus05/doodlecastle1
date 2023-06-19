using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TVevent : MonoBehaviour
{

    public Dialogue dialogue_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerMove thePlayer;
    private Inventory inventory;

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
    }

    void Update()
    {

        Debug.Log(thePlayer.notMove);
        if (Input.GetKeyDown(KeyCode.Z) && !flag && this.gameObject.ToString() == thePlayer.scanObject.ToString() && thePlayer.touch)
        {
            bool isTrue = this.gameObject.ToString() == thePlayer.scanObject.ToString();
            Debug.Log(this.gameObject.ToString());
            Debug.Log(thePlayer.scanObject.ToString());
            Debug.Log(isTrue);
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
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
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);
        flag = false;
        theOrder.Move();

    }

}
