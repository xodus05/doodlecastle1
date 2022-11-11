using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MovingObject {

    //Vector3 dirVec; //현재 바라보고있는 방향 값을 가진 변수
       /* float h;
        float v;*/
    GameObject scanObject; // 스캔 확인
    float h;
    float v;
    public int dirction;
    float detect_range = 1.5f;

    static public PlayerMove instance; // Sprite 중복 생성 방지

    public string currentMapName; // transferMap 스크립트에 있는 transferMapName 변수의 값을 저장;
    public int startPointNumber;

    public AudioClip sound; //사운드 파일
    private AudioSource audioSource; // 사운드 플레이어

    
    // private Rigidbody2D rigid2D;

    public float runSpeed; 
    private float applyRunSpeed;
    private bool applyRunFlag = false;  // 달릴 때 2칸을 가지 않기 위해 조절

    private bool canMove = true;
    Rigidbody2D rigid2D;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    void Update()
    {
/*        if (Input.GetButton("Horizontal"))
        {
            if(Input.GetAxisRaw("Horizontal") == -1)
            {
                spriteRenderer.flipX = true;
                dirction = -1;
            }

            else
            {
                spriteRenderer.flipX=false;
                dirction = 1;
            }
        }*/
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            Debug.Log("this is : " + scanObject.name);
        }
    }

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


        //Ray
        Debug.DrawRay(rigid2D.position, new Vector3(dirction*detect_range,0,0), new Color(0,0,1));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid2D.position, new Vector3(dirction,0,0), detect_range, LayerMask.GetMask("Object"));

        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
    }
}

