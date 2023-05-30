using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bookEvent : MonoBehaviour
{
    public GameObject Panel;
    private PlayerMove thePlayer;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private Inventory inventory;
    private ChoiceManager theChoice;
    public AudioManager theAudio;

    BoxCollider2D boxCollider;
    public string sound;

    private bool flag;
    private bool flag2;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theAudio = FindObjectOfType<AudioManager>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z) && !flag && thePlayer.animator.GetFloat("DirY") == 1f && flag2)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            flag = false;
            Panel.SetActive(false);
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

        theAudio.Play(sound);
        Panel.SetActive(true);

        yield return new WaitForSeconds(0.01f);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                theAudio.Play(sound);
                break;
            }
            yield return null;
        }

        Panel.SetActive(false);
        theOrder.Move();
        flag = false;
    }


}
