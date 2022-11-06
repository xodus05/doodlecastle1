using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatControll : MonoBehaviour
{

    public Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    //public Text CharacterName; // ĳ���� �̸��� ������ �ؽ�Ʈ (���� �Ⱦ���)
    public float m_Speed = 0.2f;

    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        //CharacterName.text = narrator;
        string writerText = " ";

        // �ؽ�Ʈ Ÿ���� ȿ��
        for(a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;
            yield return new WaitForSeconds(m_Speed/2);
        }
    }

    IEnumerator TextPractice()
    {
        yield return StartCoroutine(NormalChat("ĳ����1", "�߾��� ������ �����̴�. -Į�� �����"));
        //yield return StartCoroutine(NormalChat("ĳ����2", "�ȳ�..! ���� ������!"));
    }


    private void Start()
    {
        StartCoroutine(TextPractice());
    }
}
