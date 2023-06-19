using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventCollider : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Panel2;
    public Dialogue dialogue_1;

    BoxCollider2D boxCollider;
    private DialogueManager theDM;
    private crownEvent theCrown;
    private PlayerMove thePlayer;
    private CameraManager theCamera;
    private OrderManager theOrder;
    private MonsterAI theMonster;

    private bool flag;
    private bool flag2;

    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theCrown = FindObjectOfType<crownEvent>();
        thePlayer = FindObjectOfType<PlayerMove>(); // 다수 객체 리턴
        theCamera = FindObjectOfType<CameraManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theMonster = FindObjectOfType<MonsterAI>();
        flag = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) //boxCollider에 닿을때 실행되는 내장 함수 is Trigger 체크 해야함
    {
        if (collision.gameObject.name == "Player" && crownEvent.isOpen2 && flag)
        {
            StartCoroutine(EventCoroutine());
            StartCoroutine(EventCoroutine1());
            flag = false;
        }
    }

    IEnumerator EventCoroutine()
    {
        dialogue_1.sentences[0] = "저..저게 뭐야!! 빨리 문을 열고 도망가자";
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);
        theOrder.Move();
    }

    IEnumerator EventCoroutine1()
    {
        theOrder.Move();
        Panel.SetActive(true);
        Panel2.SetActive(true);

        theMonster.follow = false; // 몬스터를 멈추게 설정

        theMonster.transform.position = new Vector2(-10551, 2867);

        theMonster.follow = true; // 몬스터가 움직이도록 설정
        flag2 = true;
        yield return null;
    }
}
