using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{

/*    Vector3 dirVec; //현재 바라보고있는 방향 값을 가진 변수
    GameObject scanObject; // 스캔 확인
    float h;
    float v;*/
   
    static public PlayerMove instance; // Sprite 중복 생성 방지

    public string currentMapName; // transferMap 스크립트에 있는 transferMapName 변수의 값을 저장;
    public int startPointNumber;
    
    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    public AudioClip sound; //사운드 파일
    private AudioSource audioSource; // 사운드 플레이어

    public float speed;
    // private Rigidbody2D rigid2D;

    private Vector3 vector;

    public float runSpeed; 
    private float applyRunSpeed;
    private bool applyRunFlag = false;  // 달릴 때 2칸을 가지 않기 위해 조절

    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;

    private Animator animator;
    Rigidbody2D rigid2D;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if(instance != null) Destroy(this.gameObject);
        else {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator MoveCoroutine() {
        while(Input.GetAxisRaw("Vertical")!=0 || Input.GetAxisRaw("Horizontal") != 0)   // 연속 걷기 시 애니메이션이 계속 실행되도록
        {
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {   // SHIFT를 눌렀을 시 달리기
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }
            
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z); // 설정

            if (vector.x != 0) vector.y = 0;

            // 애니메이션 설정
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;
            // A지점, B지점
            // 레이저 (무언가 닿으면 hit = 방해물 안 닿으면 hit = null)

            //Vector2 position = rigid2D.position;
            // rigid2D.MovePosition(vector);

            Vector2 start = transform.position;  // A지점, 캐릭터 현재 위치값
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);    // B지점, 캐릭터가 이동하고자 하는 위치 값

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            if(hit.transform != null) 
                break;

            animator.SetBool("Walking", true);

            while(currentWalkCount < walkCount) {   // 한 칸을 가기 위한 반복문
                if(vector.x  != 0) {
                    transform.Translate(vector.x * (speed+applyRunSpeed), 0, 0);   // x가 0이 아니면 위치 이동
                }
                else if(vector.y != 0) {
                    transform.Translate(0, vector.y * (speed+applyRunSpeed), 0);
                }
                if(applyRunFlag) currentWalkCount++; 
                currentWalkCount++; // 한 칸을 가기 위한 반복문
                yield return new WaitForSeconds(0.01f); // 천천히 모션을 실행하기 위한 딜레이값
            }
            currentWalkCount = 0;
        }
        animator.SetBool("Walking", false);
        canMove = true;
    }

/*    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            Debug.Log("this is : " + scanObject.name);
        }

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonDown("Horizontal");
        bool vUp = Input.GetButtonDown("Vertical");

        // Direction
        if (vDown && v == 1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {

        if(canMove) {
            if(Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical") !=0) {
                canMove = false;
                StartCoroutine(MoveCoroutine());
                // rigid2D.velocity = Vector3.zero;
            }
        }
        /*rigid2D.angularVelocity = Vector3.zero;*/


/*        //Ray
        Debug.DrawRay(rigid2D.position, dirVec * 0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid2D.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;*/
    }
}

