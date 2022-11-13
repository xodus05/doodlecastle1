using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;


    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(100, new string[] { "���� ���� ��� ������ ������ �־�." }); //id���� ��� �߰�
        talkData.Add(1000, new string[] { "����� å���̴�." });
    }


    // ������ ��ȭ ������ ��ȯ�ϴ� �Լ�
    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
