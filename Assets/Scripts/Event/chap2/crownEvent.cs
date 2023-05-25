using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class crownEvent : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    public Choice choice_1;

    public GameObject Panel;


    private DialogueManager theDM;
    private ChoiceManager theChoice;
    private OrderManager theOrder;
    private Inventory inventory;

    BoxCollider2D boxCollider;

    private bool flag;
    private bool flag2;

    private static bool isOpen;
    public static bool isOpen2;



    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theOrder = FindObjectOfType<OrderManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = FindObjectOfType<Inventory>();
        if (inventory.haveItem("왕관"))
        {
            Panel.SetActive(false);
            isOpen2 = false;
        }
    }

    void Update()
    {
        if (isOpen) Panel.SetActive(true);
        if (!flag && Input.GetKeyDown(KeyCode.Z) && flag2)
        {
            flag = true;
            isOpen2 = true;
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
            theChoice.ShowChoice(choice_1);
            yield return new WaitUntil(() => !theChoice.choiceIng);

            switch (theChoice.GetResult())
            {
                case 0:
                    Panel.SetActive(true);
                    theDM.ShowDialogue(dialogue_2);
                    yield return new WaitUntil(() => !theDM.talking);
                    yield return new WaitForSeconds(0.1f);
                    isOpen2 = true;
                    break;
                case 1:
                    flag = false;
                    break;
            }
        } else
        {
            yield return new WaitForSeconds(0.1f);
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);
            yield return new WaitForSeconds(0.1f);
            isOpen2 = false;
            flag = false;
        }
        

        theOrder.Move();
    }

}
