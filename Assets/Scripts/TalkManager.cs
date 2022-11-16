using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        talkData.Add(1, new string[] { "������ ���� �ʾ�. ���� ���� �ǰ��Ѱ�... �׸� ����." }); //id???? ??? ???
        talkData.Add(2, new string[] { "�׸� �߱�?" }); //id???? ??? ???
        talkData.Add(100, new string[] { "���� ���� ��������� ������ �־�." }); //id???? ??? ???
        talkData.Add(1000, new string[] { "����� å���̾�." });
    }


    // ?????? ??? ?????? ?????? ???
    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
