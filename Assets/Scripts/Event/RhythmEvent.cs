using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmEvent : MonoBehaviour
{
    [SerializeField]
    public Dialogue Dialogue_1;
    public Dialogue Dialogue_2;

    private FadeManager theFade;
    private RhythmGame theRhythm;
    private OrderManager theOrder;
    private DialogueManager theDM;

    private bool flag;

    public string sound;
    public int turn;
    
    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
        theRhythm = FindObjectOfType<RhythmGame>();
        theOrder = FindObjectOfType<OrderManager>();
        theDM = FindObjectOfType<DialogueManager>();
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
        yield return new WaitForSeconds(0.1f);
        theDM.ShowDialogue(Dialogue_1);
        yield return new WaitUntil(()=>!theDM.talking);

        for(int i = 0; i < turn; i++) {
            theRhythm.createTile();
        }
    }
}
