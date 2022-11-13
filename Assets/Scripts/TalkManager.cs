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
        talkData.Add(1, new string[] { "이젠 정말 피곤해... 나가고 싶지 않아." }); //id값과 대사 추가
        talkData.Add(100, new string[] { "어제 먹은 배달 음식이 버려져 있어." }); //id값과 대사 추가
        talkData.Add(1000, new string[] { "평범한 책장이다." });
    }


    // 지정한 대화 문장을 반환하는 함수
    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
