using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeHEvent : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    public GameObject panel;
    public string sound;

    private static bool flag;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerMove thePlayer;
    private AudioManager theAudio;
    private FadeManager theFade;
    private BGMManager BGM;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theAudio = FindObjectOfType<AudioManager>();
        BGM = FindObjectOfType<BGMManager>();
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
        theOrder.Move("player", "UP");
        theOrder.Move("player", "UP");
        BGM.FadeOutMusic();
        theAudio.Play(sound);
        theOrder.Move("player", "DOWN");
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(()=>!theDM.talking);
        yield return new WaitForSeconds(3.0f);
        theAudio.Stop(sound);
        BGM.FadeInMusic();
        yield return new WaitForSeconds(1.0f);
        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(()=>!theDM.talking);
        panel.SetActive(true);
        theOrder.Move();
    }
}
