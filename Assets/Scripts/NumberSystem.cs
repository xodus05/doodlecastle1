using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSystem : MonoBehaviour
{

    private AudioManager theAudio;
    public string key_sound;    // 방향키 사운드
    public string enter_sound;  // 결정키 사운드
    public string cancel_sound; // 오답 && 취소키 사운드
    public string correct_sound;   // 정답 사운드

    private int count;  // 배열의 크기 (몇 자릿수인지) 0의 숫자
    private int selectedTextBox;    // 선택한 자릿수
    private int result; // 플레이어가 도출해낸 값
    private int correctNumber;  // 정답

    public GameObject superObject;  // 가운데 정렬 위함
    public GameObject[] panel;
    public Text[] Number_Text;

    public Animator anim;

    public bool activated;  // 대기 return new waitUntil

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

