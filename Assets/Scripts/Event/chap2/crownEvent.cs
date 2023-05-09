using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class crownEvent : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public GameObject Panel;
    public GameObject Panel2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private Inventory inventory;

    BoxCollider2D boxCollider;

    private bool flag;
    private bool flag2;

    private static bool isOpen;



    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (!flag && Input.GetKeyDown(KeyCode.Z) && flag2)
        {
            if (isOpen) Panel.SetActive(true);
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


        if (inventory.haveItem("왕관"))
        {
            Panel.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            theDM.ShowDialogue(dialogue_2);
            yield return new WaitUntil(() => !theDM.talking);
            yield return new WaitForSeconds(0.1f);
            isOpen = true;
        } else
        {
            yield return new WaitForSeconds(0.1f);
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);
            yield return new WaitForSeconds(0.1f);
        }
        flag = true;

        theOrder.Move();
    }

}
