using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private BGMManager BGM;

    private bool flag;

    void Start()
    {
        BGM = FindObjectOfType<BGMManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player") {
            if (!flag) {
                flag = true;
                BGM.Play(0);
                
                StartCoroutine(EventCoroutine());
            }
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기

        theOrder.NotMove();
        yield return new WaitForSeconds(0.1f);
        theDM.ShowDialogue(dialogue);
        yield return new WaitUntil(()=>!theDM.talking);


        theOrder.Move();
    }
}

