using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint; //���� �̵��Ҷ� �÷��̾��� ���� ���
    public int startPointNumber;
    private PlayerMove thePlayer;
   

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMove>();

        if(startPoint == thePlayer.currentMapName && startPointNumber == thePlayer.startPointNumber)
        {
            thePlayer.transform.position = this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
