using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addladder : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerMove thePlayer;
    private AudioManager theAudio;
    private NumberSystem theNumber;
    private Inventory inventory;
    private CameraManager theCam;
    private lockevent2 theLock;

    private static bool flag;
    private static bool flag2;
    private static bool isOpen;
    public int correctNumber;
    public GameObject Panel;
    public GameObject Panel2;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        inventory = FindObjectOfType<Inventory>();
        theAudio = FindObjectOfType<AudioManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theNumber = FindObjectOfType<NumberSystem>();
        theCam = FindObjectOfType<CameraManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        theLock = FindObjectOfType<lockevent2>();
        if (inventory.haveItem("사다리")) Panel.SetActive(false);
    }

    void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.Z) && this.gameObject.ToString() == thePlayer.scanObject.ToString() && !flag)
            {
                flag = true;
                StartCoroutine(EventCoroutine());
            }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기
        theOrder.NotMove();
        inventory.inventoryItemList.Add(new Item(5005, "사다리", Item.ItemType.Use));
        yield return new WaitForSeconds(0.1f);
        dialogue_1.sentences[0] = "사다리를 획득했다.";
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);
        theCam.Shake();
        yield return new WaitForSeconds(0.1f);
        theDM.ShowDialogue(dialogue_2);
        Panel.SetActive(false);
        yield return new WaitUntil(() => !theDM.talking);
        yield return new WaitForSeconds(1f); // 1초간 카메라 흔들림 유지
        theCam.StopShake();
        yield return new WaitForSeconds(0.2f);
        theDM.ShowDialogue(dialogue_3);
        theAudio.Play("dropkey");
        yield return new WaitUntil(() => !theDM.talking);
        //Camera.main.GetComponent<Camshake>().Shake();
        theOrder.Move();
        Panel2.SetActive(false);
    }
}
