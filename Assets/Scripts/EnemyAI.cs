using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{

    public static EnemyAI instance;

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

    Rigidbody2D rb;
    Transform target;
    public GameObject Panel;

    private AudioManager theAudio;

    [SerializeField] [Range(1f, 500f)] float moveSpeed = 300f;

    [SerializeField] [Range(0f, 3f)] float contactDistance = 1f;

    //public bool isStopped = false; // 멈춤 상태 저장
    public Vector2 startPosition;
    public bool follow = false;
    public bool isFirst = false;

    public void Start() {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startPosition = transform.position;
    }

    void Update() {
        FollowTarget();
    }

    void FollowTarget() {
            if (Vector2.Distance(transform.position, target.position) > contactDistance && follow)
            {
                Panel.transform.position = Vector2.MoveTowards(Panel.transform.position, target.position, moveSpeed * Time.deltaTime);
                isFirst = true;
            }
            else
            {
                if (isFirst)
                {
                    Panel.transform.position = Vector2.MoveTowards(Panel.transform.position, target.position, (moveSpeed) * Time.deltaTime);
                }
                //rb.velocity = Vector2.zero;
            }
    }

/*    private void OnTriggerEnter2D(Collider2D other) {
        follow = true;
    }*/

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // 충돌한 게임 오브젝트의 태그가 "Player"인 경우
        {
            follow = true;
            startPosition = transform.position; // 충돌 시 현재 위치를 저장합니다.
            SceneManager.LoadScene("Died"); // Died 씬으로 이동합니다.
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") // 충돌이 끝난 게임 오브젝트의 태그가 "Player"인 경우
        {
            Panel.transform.position = startPosition; // 충돌이 끝나면 저장된 위치로 되돌아갑니다.
        }
    }
}

