using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{

    private Inventory inventory;
    private crownEvent theCrown;

    public static MonsterAI instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    Rigidbody2D rb;
    Transform target;
    public GameObject Panel;

    private AudioManager theAudio;

    [SerializeField][Range(1f, 500f)] float moveSpeed = 300f;

    [SerializeField][Range(0f, 3f)] float contactDistance = 1f;

    bool follow = false;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        rb = GetComponent<Rigidbody2D>();
        theCrown = FindObjectOfType<crownEvent>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        //if (crownEvent.isOpen2) follow = true;
        if (Vector2.Distance(transform.position, target.position) > contactDistance && follow)
        {
            Panel.transform.position = Vector2.MoveTowards(Panel.transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (follow)
            {
                Panel.transform.position = Vector2.MoveTowards(Panel.transform.position, target.transform.position, (moveSpeed) * Time.deltaTime);
            }
            //rb.velocity = Vector2.zero;
        }
    }

}