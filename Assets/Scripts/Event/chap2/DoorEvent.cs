using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEvent : MonoBehaviour
{

    private DialogueManager theDM;
    private OrderManager theOrder;
    private Inventory inventory;
    private crownEvent theCrown;
    private PlayerMove thePlayer;
   

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
        theCrown = FindObjectOfType<crownEvent>();
    }

    void Update()
    {
        if (!flag && Input.GetKeyDown(KeyCode.Z) && flag2)
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
                currentKeyPresses++;
                Debug.Log("현재 키 입력 횟수: " + currentKeyPresses);

                if (currentKeyPresses >= requiredKeyPresses)
                {
                    Debug.Log("문이 열립니다.");
                    SceneManager.LoadScene("castle");
                    thePlayer.transform.position = new Vector2(-6096, -1982);
                    yield break;
                }
            }

            yield return null;
        }
    }

}
