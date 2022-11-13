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
            thePlayer.canMove = false;
            isAction =true;
            scanObject = scanObj;
            Objdata objData = scanObject.GetComponent<Objdata>();
            //talkText.text = "이것의 이름은 " + scanObject.name + "이야ㅗㅗㅗ";
            Talk(objData.id, objData.isNpc);
        }
        talkPanel.SetActive(isAction); //함수 숨기기 보여주기 구현
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        if (isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }
    }

}
