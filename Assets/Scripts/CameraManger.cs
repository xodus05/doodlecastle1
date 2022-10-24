using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManger : MonoBehaviour
{

    static public CameraManger instance;

    public GameObject target; //카메라가 따라갈 대상.
    public float moveSpeed; //카메라가 얼마나 빠른 속도로 따라갈것인지
    private Vector3 targetPosition; //대상의 현재 위치 값



    // Start is called before the first frame update
    void Start()
    {
        if(instance != null) Destroy(this.gameObject);
        else {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            // Time.deltaTime 1초에 movespeed만큼 이동
            this.transform.position = Vector3.Lerp(this.targetPosition, targetPosition, moveSpeed * Time.deltaTime);

        }
    }

}
