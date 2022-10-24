using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; //이동할 맵의 이름
    public int startPointNumber;

    private PlayerMove thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMove>(); // 다수 객체 리턴
        //GetComponent //단일 객체;
    }

    private void OnTriggerEnter2D(Collider2D collision) //boxCollider에 닿을때 실행되는 내장 함수 is Trigger 체크 해야함
    {
        if(collision.gameObject.name == "Player") //boxCollider에 닿은 객체 이름(player)가 닿았을때 조건문 실행
        {
            thePlayer.startPointNumber = startPointNumber;
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName); // 이동할 맵의 이름으로 이동
        }    
    }


}
