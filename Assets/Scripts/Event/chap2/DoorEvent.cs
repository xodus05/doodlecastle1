using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DoorEvent : MonoBehaviour
{
    private FadeManager theFade;
    private DialogueManager theDM;
    private OrderManager theOrder;
    private Inventory inventory;
    private crownEvent theCrown;
    private PlayerMove thePlayer;
    public AudioManager theAudio;

    public string sound;

    public GameObject Panel;

    BoxCollider2D boxCollider;

    private bool flag;
    private bool flag2;


    public int requiredKeyPresses = 10; // 문을 열기 위해 필요한 키 입력 횟수
    private int currentKeyPresses = 0; // 현재까지 입력한 키 횟수zz

    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = FindObjectOfType<Inventory>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theFade = FindObjectOfType<FadeManager>();
        theCrown = FindObjectOfType<crownEvent>();
        theAudio = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (!flag && Input.GetKeyDown(KeyCode.Z) && flag2 && crownEvent.isOpen2)
        {
            flag = true;
            StartCoroutine(CountKeyPresses());
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CountKeyPresses());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            currentKeyPresses = 0;
        }
    }

    private IEnumerator CountKeyPresses()
    {
        while (true)
            {

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    theAudio.Play(sound);
                    currentKeyPresses++;
                    Debug.Log("현재 키 입력 횟수: " + currentKeyPresses);

                    if (currentKeyPresses >= requiredKeyPresses)
                    {
                        Panel.SetActive(true);
                        theFade.Flash(0.01f);
                    yield return new WaitForSeconds(1f);
                    Debug.Log("문이 열립니다.");


                        
                        SceneManager.LoadScene("castle");
                        thePlayer.transform.position = new Vector2(-6096, -1982);
                        //crownEvent.isOpen2 = false; // isOpen을 false로 설정
                    }
                }
                yield return null;
            }
        }

/*    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기
        theOrder.NotMove();


        if (crownEvent.isOpen2)
        {

            dialogue_1.sentences[0] = "저..저게 뭐야! 어서 문으로 가자";
            theDM.ShowDialogue(dialogue_1); // 대사를 나타내는 대화창을 활성화
            yield return new WaitForSeconds(3f); // 3초 대기

            Panel1.SetActive(true);
            Panel2.SetActive(true);

            yield return new WaitForSeconds(3f);
        }



        theOrder.Move();
    }*/

}
