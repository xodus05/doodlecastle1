using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map1event : MonoBehaviour
{
    [SerializeField]
    public Dialogue Dialogue_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private PlayerMove thePlayer;

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theChoice = FindObjectOfType<ChoiceManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기

        theOrder.NotMove();

        yield return new WaitForSeconds(0.5f);

        theOrder.Move("player", "UP");
        yield return new WaitUntil(()=>thePlayer.queue.Count == 0);

        theDM.ShowDialogue(Dialogue_1);
        yield return new WaitUntil(()=>!theDM.talking);

        theOrder.Move();
    }
}
