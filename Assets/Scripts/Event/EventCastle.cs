using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCastle : MonoBehaviour
{
    [SerializeField]
    public Dialogue Dialogue_1;
    public Dialogue Dialogue_2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerMove thePlayer;   // DirY == 1f

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag)
        {
            flag = true;
            if (collision.gameObject.name == "Player")
            {
                theDM.ShowDialogue(Dialogue_1);
            }
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharater(); // 리스트 채우기

        theOrder.NotMove();

        theOrder.Move();
    }
}
