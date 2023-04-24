using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmEvent : MonoBehaviour
{
    [SerializeField]
    public Dialogue Dialogue_1;
    public Dialogue Dialogue_2;
    public Dialogue Dialogue_c;
    public Dialogue Dialogue_n;

    private FadeManager theFade;
    private RhythmGame theRhythm;
    private OrderManager theOrder;
    private DialogueManager theDM;

    private string arrowPush;
    private string myAnswer; // 내가 입력한 값

    private bool flag;
    private bool flag2;
    private bool go;
    private int c = 2;

    public string sound;
    public int turn;
    public Text arrowText;

    public GameObject panel;

    
    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
        theRhythm = FindObjectOfType<RhythmGame>();
        theOrder = FindObjectOfType<OrderManager>();
        theDM = FindObjectOfType<DialogueManager>();
        arrowPush = "";
    }

    void Update()
    {
        if (flag2)
        {

            if (arrowPush.Length == c)
            {
                Debug.Log("내 답 : " + myAnswer);
                Debug.Log("정답 : " + theRhythm.correctNumber.ToString());
                go = true;
                arrowPush = "";
                flag2 = false;

            }
            // Direction
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                arrowPush += "↑";
                myAnswer += "1";
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                arrowPush += "↓";
                myAnswer += "2";
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                arrowPush += "←";
                myAnswer += "4";
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                arrowPush += "→";
                myAnswer += "3";
            }
            arrowText.text = arrowPush;
        }
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
        panel.SetActive(true);
        for(int i = 0; i < turn; i++) {
            go = false;
            for (int j = 0; j < c; j++)
            {
                theRhythm.createTile();
                yield return new WaitForSeconds(0.6f);
            }
            flag2 = true;
            yield return new WaitUntil(() => go);
            if(theRhythm.correctNumber.ToString().Equals(myAnswer))
            {
                theDM.ShowDialogue(Dialogue_c);
                yield return new WaitUntil(() => !theDM.talking);
                c++;
            }
            else
            {
                theDM.ShowDialogue(Dialogue_n);
                yield return new WaitUntil(() => !theDM.talking);
                i--;
            }
            myAnswer = "";
            theRhythm.correctNumber = 0;
            arrowText.text = arrowPush;
        }
        theDM.ShowDialogue(Dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);
    }
}