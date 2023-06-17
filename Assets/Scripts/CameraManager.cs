using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public GameObject target; // 카메라가 따라갈 대상.
    public float moveSpeed; // 카메라가 얼마나 빠른 속도로 따라갈 것인지
    private Vector3 targetPosition; // 대상의 현재 위치 값

    public BoxCollider2D bound;
    private Vector3 minBound;
    private Vector3 maxBound;
    // 박스 컬라이더 영역의 최소 최대 xyz값을 지님

    private float halfWidth;
    private float halfHeight;
    // 카메라의 반너비, 반높이 값을 지닐 변수

    private Camera theCamera;

    // 카메라 흔들기
    [SerializeField] float shakeMagnitude = 0.1f;
    [SerializeField] float shakeDuration = 0.1f;
    private Vector3 initialPosition;
    public Vector3 originalPosition; // 줌인 오브젝트의 원래 위치 저장

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
        {
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
        if (target != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            // Time.deltaTime 1초에 movespeed만큼 이동
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);    // Lerp : A값과 B값 사이의 선형 보간으로 중간 값을 리턴

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
           
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }

    public void Shake()
    {
        initialPosition = transform.localPosition;
        StartCoroutine(DoShake());
    }

    public void StopShake()
    {
        StopAllCoroutines();
        transform.localPosition = initialPosition;
    }

    public void SetShakeMagnitude(float magnitude)
    {
        shakeMagnitude = magnitude;
    }

    public void SetCameraFollow(GameObject newTarget)
    {
        target = newTarget;
    }

    public void ZoomIn(Vector3 zoomTarget)
    {
        StartCoroutine(Zoom(zoomTarget));
    }

 

    IEnumerator DoShake()
    {
        float elapsedTime = 0f;
        Vector3 playerPosition = target.transform.position; // 플레이어의 위치 저장

        while (elapsedTime < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // 플레이어 위치를 기준으로 흔들림 벡터 생성
            Vector3 shakeVector = new Vector3(playerPosition.x + x, playerPosition.y + y, targetPosition.z);
            transform.localPosition = shakeVector;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = targetPosition;
    }

    IEnumerator Zoom(Vector3 zoomTarget)
    {
        float targetOrthographicSize = 200f; // 줌인할 목표 Orthographic Size (원하는 값으로 변경)

        float initialOrthographicSize = theCamera.orthographicSize;
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = new Vector3(zoomTarget.x, zoomTarget.y, transform.position.z);

        while (elapsedTime < 2f) // 1초 동안 줌인 애니메이션 진행
        {
            float t = elapsedTime / 2f; // 진행 시간에 따른 보간 비율 계산
            theCamera.orthographicSize = Mathf.Lerp(initialOrthographicSize, targetOrthographicSize, t);
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        theCamera.orthographicSize = targetOrthographicSize; // 최종적으로 목표 Orthographic Size로 설정
        transform.position = targetPosition; // 최종적으로 목표 위치로 설정
    }
}
