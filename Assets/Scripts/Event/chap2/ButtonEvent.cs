using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonEvent : MonoBehaviour
{
    private Inventory inventory;

    public string transferMapName; //이동할 맵의 이름
    public int startPointNumber;

    public string sound;

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
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theAudio = FindObjectOfType<AudioManager>();
        inventory = FindObjectOfType<Inventory>();
        theFade = FindObjectOfType<FadeManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag && Input.GetKey(KeyCode.Z))
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }
        IEnumerator EventCoroutine()
        {
        theOrder.NotMove();
        yield return new WaitForSeconds(0.1f);
        theOrder.PreLoadCharacter(); // 리스트 채우기
        if (inventory.haveItem("도서관 열쇠"))
        {
            if (isFirst)
            {
                theAudio.Play(sound);
                theDM.ShowDialogue(dialogue_2);
                yield return new WaitUntil(() => !theDM.talking);
                isFirst = false;
            }
            theFade.FadeOut();
            yield return new WaitForSeconds(1f);
            thePlayer.startPointNumber = startPointNumber;
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName); // 이동할 맵의 이름으로 이동
            theFade.FadeIn();
        }
        else
        {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);
            yield return new WaitForSeconds(0.4f);
        }
        flag = false;
        theOrder.Move();
    }
}
