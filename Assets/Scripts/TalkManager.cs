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
        talkData.Add(1, new string[] { "너무 피곤해. 이젠 나가고 싶지 않아... 그만 자자." }); //id???? ??? ???
        talkData.Add(2, new string[] { "포탈이 막혀있어...돌아갈 수 없잖아." }); //id???? ??? ???
        talkData.Add(100, new string[2] { "어제 먹고 남은 쓰레기가 남아있어.", "치킨 먹고 싶네..." }); //id???? ??? ???
        talkData.Add(1000, new string[2] { "평범한 옷장이야.", "최근에 책을 읽은 적이 없어서 뭐가 있는지도 모르겠다." });
    }


    // ?????? ??? ?????? ?????? ???
    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }

    }
}
