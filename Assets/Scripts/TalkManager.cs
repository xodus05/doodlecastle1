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
        talkData.Add(4, new string[] { "순서는 도토리, 나뭇잎, 모양이 다른 나무 개수와, \n그리고(지워져서 읽을 수 없다.)" });
        talkData.Add(5, new string[] { "죽은 나무의 가지 수가 마지막이야" });
        talkData.Add(6, new string[] { "도토리와 다람쥐다." });
        talkData.Add(8, new string[] { "나무가...비현질적으로 잘려져 있어. \n정말 내가 그림 안으로 들어왔다고..." });
        talkData.Add(10, new string[] { "무덤...?이라고 써져있어. 소름끼치네." }); 
        talkData.Add(20, new string[] { "철창이 있어. 나가긴 힘들어 보이네..." }); 
        talkData.Add(30, new string[] { "여기 온 자들은 다 죽었어. \n너도 그렇게 될걸." }); 
        talkData.Add(40, new string[] { "쓰레기통엔 잿더미밖에 없어." }); 
        talkData.Add(50, new string[] { "웬 장작더미지? 캠프파이어라도 했나." }); 
        talkData.Add(100, new string[] { "어제 먹고 버린 쓰레기가 남아있어."}); //id???? ??? ???
        talkData.Add(1000, new string[] { "평범한 책장이야."});
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
