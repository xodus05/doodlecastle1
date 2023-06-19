using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bedEvent : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue_1;
    public Choice choice_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private PlayerMove thePlayer;
    private BGMManager BGM;

    public GameObject Panel;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;

    BoxCollider2D boxCollider;

    public string sound;

    private AudioManager theAudio;

    private bool flag;
    private bool flag2;



    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theAudio = FindObjectOfType<AudioManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        BGM = FindObjectOfType<BGMManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !flag && thePlayer.animator.GetFloat("DirX") == -1f && flag2)
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
        theOrder.PreLoadCharacter(); // 리스트 채우기

        theOrder.NotMove();
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = false;
        theChoice.ShowChoice(choice_1);
        yield return new WaitUntil(() => !theChoice.choiceIng);
        if(theChoice.GetResult()==0) {
            theOrder.Move("player", "LEFT");
            yield return new WaitUntil(()=>thePlayer.queue.Count == 0);
            yield return new WaitForSeconds(1.0f);
            theAudio.Play(sound);
            BGM.Stop(0);
            yield return new WaitForSeconds(3.0f);
            // 부스럭 거리는 효과음 넣기

            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(()=>!theDM.talking);
            theOrder.Move("player", "RIGHT");
            yield return new WaitForSeconds(0.2f);
            Panel.SetActive(true);
            Panel2.SetActive(false);
            Panel3.SetActive(false);
            Panel4.SetActive(true);
        }
        else {
            flag = false;
            yield return new WaitForSeconds(0.2f);
        }
        boxCollider.enabled = true;
        theOrder.Move();
    }
}
