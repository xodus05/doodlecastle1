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
        talkData.Add(1, new string[] { "나가고 싶지 않아. 이젠 정말 피곤한걸... 그만 자자." }); //id???? ??? ???
        talkData.Add(2, new string[] { "그만 잘까?" }); //id???? ??? ???
        talkData.Add(100, new string[] { "어제 먹은 배달음식이 버려져 있어." }); //id???? ??? ???
        talkData.Add(1000, new string[] { "평범한 책장이야." });
    }


    // ?????? ??? ?????? ?????? ???
    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
