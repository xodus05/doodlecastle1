using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{

    private BoxCollider2D bound;

    private CameraManager theCamera;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision) //boxCollider에 닿을때 실행되는 내장 함수 is Trigger 체크 해야함
    {
        if(collision.gameObject.name == "Player") //boxCollider에 닿은 객체 이름(player)가 닿았을때 조건문 실행 
        {
        bound = GetComponent<BoxCollider2D>();
        theCamera = FindObjectOfType<CameraManager>();
        theCamera.SetBound(bound);
        }
    }
}
