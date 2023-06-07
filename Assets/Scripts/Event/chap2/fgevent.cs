using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fgevent : MonoBehaviour
{

    public Dialogue dialogue_1;
    public GameObject Panel;

    private DialogueManager theDM;
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
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = FindObjectOfType<Inventory>();
        control = FindObjectOfType<controlEvent>();
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
        if(collision.gameObject.name == "Player")
            flag2 = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // if(collision.gameObject.name == "Player")
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
        yield return new WaitForSeconds(0.1f);
    }

}
