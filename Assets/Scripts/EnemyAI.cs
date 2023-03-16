using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool isStopped = false; // ∏ÿ√„ ªÛ≈¬ ¿˙¿Â
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
        if (isStopped) {
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
    }

    private void OnTriggerEnter2D(Collider2D other) {
        follow = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name == "Player")
        {
            transform.position = startPosition;
        }
    }
}

