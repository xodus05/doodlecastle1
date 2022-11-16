using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ChoiceController : MonoBehaviour
{
    public TalkManager talkManager;
    public Text ChatText; //실제 채팅이 나오는 텍스트

    public string writerText = "";
    Dictionary<int, string[]> talkData;

    //선택지 2개 
    public GameObject yes;
    public GameObject no;

    void Start()
    {
        yes.SetActive(false);
        no.SetActive(false);

        StartCoroutine(TextPractice());
    }

    IEnumerator NormalChat(string narrator)
    {

        int a = 0;
        //CharacterName.text = narrator;
        string writerText = " ";


        while (true)
        {
            yield return null;
        }
    }

    IEnumerator TextPractice()
    {
        yield return StartCoroutine(NormalChat("그만 잘까?"));
        talkData.Add(2, new string[] { "그만 잘까?" }); //id???? ??? ???
        yes.SetActive(true);
        no.SetActive(true);
    }
}
