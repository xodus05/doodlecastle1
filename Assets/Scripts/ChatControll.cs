using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatControll : MonoBehaviour
{

    public Text ChatText; // 실제 채팅이 나오는 텍스트
    //public Text CharacterName; // 캐릭터 이름이 나오는 텍스트 (아직 안쓸듯)
    public float m_Speed = 0.2f;

    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        //CharacterName.text = narrator;
        string writerText = " ";

        // 텍스트 타이핑 효과
        for(a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;
            yield return new WaitForSeconds(m_Speed/2);
        }
    }

    IEnumerator TextPractice()
    {
        yield return StartCoroutine(NormalChat("캐릭터1", "추억은 일종의 만남이다. -칼릴 지브란"));
        //yield return StartCoroutine(NormalChat("캐릭터2", "안녕..! 구현 연습중!"));
    }


    private void Start()
    {
        StartCoroutine(TextPractice());
    }
}
