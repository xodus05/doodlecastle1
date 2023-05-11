using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    private Inventory inventory;

    public static Monster instance;

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
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (Vector2.Distance(transform.position, target.position) > contactDistance && follow)
        {
            Panel.transform.position = Vector2.MoveTowards(Panel.transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (follow)
            {
                Panel.transform.position = Vector2.MoveTowards(Panel.transform.position, target.position, (moveSpeed) * Time.deltaTime);
            }
            //rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene("Died1");
        }
    }
}