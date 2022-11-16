using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ChoiceController : MonoBehaviour
{
    public TalkManager talkManager;
    public Text ChatText; //���� ä���� ������ �ؽ�Ʈ

    public string writerText = "";
    Dictionary<int, string[]> talkData;

    //������ 2�� 
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
        yield return StartCoroutine(NormalChat("�׸� �߱�?"));
        talkData.Add(2, new string[] { "�׸� �߱�?" }); //id???? ??? ???
        yes.SetActive(true);
        no.SetActive(true);
    }
}
