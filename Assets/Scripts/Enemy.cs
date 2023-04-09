using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;
    public EnemyAI enemyai;

    public Vector2 startPosition;

    public void Start()
    {
        enemyai = FindObjectOfType<EnemyAI>();
        startPosition = transform.position; // 몬스터의 초기 위치를 저장
    }

    #region Singleton
    private void Awake() {
         if(instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
         }
         else {
            Destroy(this.gameObject);
         }
    }
    #endregion Singleton

/*     private void OnTriggerStay2D(Collider2D collision)
    {

            if (collision.gameObject.name == "Player")
            {
            SceneManager.LoadScene("Died"); //quote 로 scene 이동
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }*/

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // 충돌한 게임 오브젝트의 태그가 "Player"인 경우
        {
            SceneManager.LoadScene("Died"); //quote 로 scene 이동
            // 충돌 시 현재 위치를 저장합니다.
            //startPosition = transform.position;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player") // 충돌이 끝난 게임 오브젝트의 태그가 "Player"인 경우
        {
            // 충돌이 끝나면 저장된 위치로 되돌아갑니다.
            //transform.position = startPosition;
        }
    }
}
