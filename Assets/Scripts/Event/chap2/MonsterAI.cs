using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{

    private Inventory inventory;
    private crownEvent theCrown;

    private DialogueManager theDM;

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
    Transform target1;
    public GameObject Panel;
    public Dialogue dialogue_1;
    public DoorEvent door;

    private AudioManager theAudio;

    [SerializeField][Range(1f, 500f)] float moveSpeed = 300f;

    [SerializeField][Range(0f, 3f)] float contactDistance = 1f;

    public bool follow = false;

    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        inventory = FindObjectOfType<Inventory>();
        rb = GetComponent<Rigidbody2D>();
        theCrown = FindObjectOfType<crownEvent>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        door = FindObjectOfType<DoorEvent>();
    }

    public void Update()
    {
        follow = true;
        FollowTarget();
    }

    public void FollowTarget()
    {
        if(door.isOpen) follow = false;
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

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("End");
        follow = false;
    }
}