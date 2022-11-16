using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : MonoBehaviour 
{
    [Tooltip("대사 내용")]
    public string[] contexts;

    [Tooltip("이벤트 번호")]
    public string[] number;

    [Tooltip("스킵라인")]
    public string[] skipnum;
}

[System.Serializable]
public class DialgoueEvent
{
    public string name; //이벤트 이름
    public DialgoueEvent[] dialgoues;
}