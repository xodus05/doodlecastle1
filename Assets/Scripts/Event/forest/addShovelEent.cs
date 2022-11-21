using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addShovelEent : MonoBehaviour
{

    public GameObject Panel;
    public Dialogue dialogue_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private PlayerMove thePlayer;

    private static bool flag;

    void Start()
    {
        theChoice = FindObjectOfType<ChoiceManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag && Input.GetKey(KeyCode.Z))
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.NotMove();

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(()=>!theDM.talking);

        Panel.SetActive(false);
        theOrder.Move();
    }

}
