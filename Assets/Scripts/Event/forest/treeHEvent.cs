using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeHEvent : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    public GameObject panel;
    public string sound;

    private bool flag;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerMove thePlayer;
    private AudioManager theAudio;
    private FadeManager theFade;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theAudio = FindObjectOfType<AudioManager>();
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
        theOrder.Move("player", "UP");
        theOrder.Move("player", "UP");
        theAudio.Play(sound);
        theOrder.Move("player", "DOWN");
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(()=>!theDM.talking);
        yield return new WaitForSeconds(3.0f);
        theAudio.Stop(sound);
        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(()=>!theDM.talking);
        panel.SetActive(true);
        theOrder.Move();
    }
}
