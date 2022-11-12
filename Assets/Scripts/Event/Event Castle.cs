using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCastle : MonoBehaviour
{

    //public Dialogue Dialogue_1;
    //public Dialogue Dialogue_2;

    //private Dialogue theDM;
    private OrderManager theOrder;
    private PlayerMove thePlayer;   // DirY == 1f

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        // theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
/*    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag && Input.GetKey(KeyCode.Space) && thePlayer.animator.GetFloat("DirY") == 1f)
        {
            flag = true;    // 다시 실행되지 않게
            EventCoroutine();
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharater();

        theOrder.Move();
    }*/
}
