using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openEvent2 : MonoBehaviour
{
    private Inventory inventory;

    public string transferMapName; //이동할 맵의 이름
    public int startPointNumber;

    public string sound;
    public string sound1;

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private static bool flag;
    private static bool isFirst = true;

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
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !flag && thePlayer.scanObject && this.gameObject.ToString()==thePlayer.scanObject.ToString())
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
        if(inventory.haveItem("열쇠")) {
            if(isFirst) {
                theAudio.Play(sound1);
                theDM.ShowDialogue(dialogue_2);
                yield return new WaitUntil(()=>!theDM.talking);
                isFirst = false;
            }
            theAudio.Play(sound);
            theFade.FadeOut();
            yield return new WaitForSeconds(1f);
            thePlayer.startPointNumber = startPointNumber;
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName); // 이동할 맵의 이름으로 이동
            theFade.FadeIn();
        }
        else {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(()=>!theDM.talking);
            yield return new WaitForSeconds(0.4f);
        }
        flag = false;
        theOrder.Move();
    }
}

