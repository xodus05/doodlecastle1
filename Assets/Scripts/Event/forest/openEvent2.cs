using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openEvent2 : MonoBehaviour
{
    public string transferMapName; //이동할 맵의 이름
    public int startPointNumber;

    public string sound;

    public Dialogue dialogue_1;

    private static bool flag;

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
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!flag && Input.GetKey(KeyCode.Z) && thePlayer.animator.GetFloat("DirY") == 1f)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기
        theOrder.NotMove();
        if(thePlayer.haveKey) {
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
