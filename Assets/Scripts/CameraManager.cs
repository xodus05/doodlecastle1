using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public GameObject target; //카메라가 따라갈 대상.
    public float moveSpeed; //카메라가 얼마나 빠른 속도로 따라갈것인지
    private Vector3 targetPosition; //대상의 현재 위치 값

    public BoxCollider2D bound;

    private Vector3 minBound;
    private Vector3 maxBound;
    // 박스 컬라이더 영역의 최소 최대 xyz값을 지님

    private float halfWidth;
    private float halfHeight;
    // 카메라의 반너비, 반높이 값을 지닐 변수

    private Camera theCamera;

    private void Awake() {
        if(instance != null) 
            Destroy(this.gameObject);
        else {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
       theCamera = GetComponent<Camera>();
       minBound = bound.bounds.min;
       maxBound = bound.bounds.max;
       halfHeight = theCamera.orthographicSize;
       halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            // Time.deltaTime 1초에 movespeed만큼 이동
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);    // Lerp : A값과 B값 사이의 선형 보간으로 중간 값을 리턴

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
    }

    public void SetBound(BoxCollider2D newBound) {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
