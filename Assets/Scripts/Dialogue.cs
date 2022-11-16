using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : MonoBehaviour 
{
    [Tooltip("��� ����")]
    public string[] contexts;

    [Tooltip("�̺�Ʈ ��ȣ")]
    public string[] number;

    [Tooltip("��ŵ����")]
    public string[] skipnum;
}

[System.Serializable]
public class DialgoueEvent
{
    public string name; //�̺�Ʈ �̸�
    public DialgoueEvent[] dialgoues;
}