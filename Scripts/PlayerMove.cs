using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    public float speed;

    private Vector3 vector;

    public float runSpeed; 
    private float applyRunSpeed;
    private bool applyRunFlag = false;  // 달릴 때 2칸을 가지 않기 위해 조절

    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
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

            if(vector.x != 0) vector.y = 0;

            // 애니메이션 설정
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
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

    // Update is called once per frame
    void Update()
    {
        if(canMove) {
            if(Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical") !=0) {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
