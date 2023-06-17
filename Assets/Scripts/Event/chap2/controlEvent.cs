using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlEvent : MonoBehaviour
{

    public Dialogue dialogue_1;
    public GameObject Panel;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerMove thePlayer;
    private Inventory inventory;

    BoxCollider2D boxCollider;
    private bool activated;

    private bool flag;
    private bool flag2;
    public static bool isOpen;



    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = FindObjectOfType<Inventory>();
        if(isOpen) Panel.SetActive(true);
    }

    void Update()
    {
        if (!flag && Input.GetKeyDown(KeyCode.Z) && flag2 && thePlayer.touch)
        {
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
        yield return new WaitForSeconds(0.1f);
        isOpen = true;
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);
        yield return new WaitForSeconds(0.1f);

        Panel.SetActive(true);
        flag = false;
        theOrder.Move();
    }

}
