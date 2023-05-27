        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shovelEvent : MonoBehaviour
{
    private Inventory inventory;

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;

    public Choice choice_1;

    public string sound;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private PlayerMove thePlayer;
    private FadeManager theFade;
    private AudioManager theAudio;
    private BGMManager BGM;
    private title title;

    private static bool flag;
    private static bool flag2;

    public GameObject Panel;
    public GameObject Panel2;

    void Start()
    {
        theChoice = FindObjectOfType<ChoiceManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theFade = FindObjectOfType<FadeManager>();
        theAudio = FindObjectOfType<AudioManager>();
        inventory = FindObjectOfType<Inventory>();
        title = FindObjectOfType<title>();
        BGM = FindObjectOfType<BGMManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !flag && thePlayer.animator.GetFloat("DirY") == 1f && flag2)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            flag2 = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            flag2 = false;
    }

    IEnumerator EventCoroutine()
    {
        theOrder.NotMove();
        yield return new WaitForSeconds(0.1f);
        if(inventory.haveItem("삽")) {
            theChoice.ShowChoice(choice_1);
            yield return new WaitUntil(() => !theChoice.choiceIng);
            switch(theChoice.GetResult()) {
            case 0 : 
                theDM.ShowDialogue(dialogue_2);
                yield return new WaitUntil(()=>!theDM.talking);
                theFade.Flash();
                Panel.SetActive(true);
                Panel2.SetActive(true);
                inventory.activeList.Add("라이터");
                theAudio.Play(sound);
                theDM.ShowDialogue(dialogue_3);
                yield return new WaitUntil(()=>!theDM.talking);
                BGM.Play(4);
                theAudio.Stop(sound);
            break;
            case 1 :
                flag = false;
            break;
            }
        }
        else {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(()=>!theDM.talking);
            flag = false;
        }
        theOrder.Move();
    }
}
