using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    private PlayerMove thePlayer;
    public GameObject talkPanel;
    public Text talkText;
    public int talkIndex;
    public GameObject scanObject;
    public bool isAction; //상태 변수값
    public Text text;
    public Animator animSprite;
    public Animator animDialogueWindow;

    public static GameManager instance;

    #region Singleton
    private void Awake() {
         if(instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
         }
         else {
            Destroy(this.gameObject);
         }
    }
    #endregion Singleton

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMove>();
    }

    public void Action(GameObject scanObj)
    {
        if (isAction) // Exit Action
        {
            isAction = false;
            thePlayer.canMove = true;
        }
        else // Enter Action
        {
            isAction =true;
            scanObject = scanObj;
            Objdata objData = scanObject.GetComponent<Objdata>();
            if(objData != null) {
                Talk(objData.id, objData.isNpc);
            }
            else {
                return;
            }
        }
        //talkPanel.SetActive(isAction); //함수 숨기기 보여주기 구현
        if(isAction) {
            animSprite.SetBool("Appear", true);
            animDialogueWindow.SetBool("Appear", true); // 대화창 등장
        }
        else {
            text.text = "";
            animSprite.SetBool("Appear", false);
        animDialogueWindow.SetBool("Appear", false); // 대화창 삭제
        }
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
        }
        thePlayer.canMove = false;
        talkData = talkManager.GetTalk(id, talkIndex);
        talkText.text = talkData;

        isAction = true;
        talkIndex++;
    }

}
