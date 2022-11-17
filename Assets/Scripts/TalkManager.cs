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
        DontDestroyOnLoad(this.gameObject);
    }

    void GenerateData()
    {
        talkData.Add(1, new string[] { "너무 피곤해. 이젠 나가고 싶지 않아... 그만 자자." }); //id???? ??? ???
        talkData.Add(2, new string[] { "포탈이 막혀있어...돌아갈 수 없잖아." }); //id???? ??? ???
        talkData.Add(3, new string[] { "나뭇잎 그림에 숫자가 써 있어... 5?" });
        talkData.Add(4, new string[] { "순서는 도토리, 나뭇잎, 모양이 다른 나무, 그리고\n(지워져서 읽을 수 없다.)" });
        talkData.Add(5, new string[] { "오른쪽 나무의 가지 수를 봐" });
        talkData.Add(6, new string[] { "도토리와 다람쥐다." });
        talkData.Add(10, new string[] { "돌탑이다! 어릴 때 내가 쌓았던 거랑 비슷하게 생겼네..." }); 
        talkData.Add(20, new string[] { "철창이 있어. 나가긴 힘들어 보이네..." }); 
        talkData.Add(30, new string[] { "무덤이다. 공동묘지였던걸까..." }); 
        talkData.Add(100, new string[2] { "어제 먹고 남은 쓰레기가 남아있어.", "치킨 먹고 싶네..." }); //id???? ??? ???
        talkData.Add(1000, new string[2] { "평범한 책장이야.", "최근에 책을 읽은 적이 없어서 뭐가 있는지도 모르겠다." });
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
