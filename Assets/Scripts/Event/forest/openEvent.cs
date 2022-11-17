using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openEvent : MonoBehaviour
{
    public string transferMapName; //이동할 맵의 이름
    public int startPointNumber;

    public GameObject Panel;
    public string sound;

    private bool flag;

    private OrderManager theOrder;
    private PlayerMove thePlayer;
    private AudioManager theAudio;
    private FadeManager theFade;

    // Start is called before the first frame update
    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theAudio = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag && Input.GetKey(KeyCode.Z))
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기
        theOrder.NotMove();
        Panel.SetActive(false);
        theAudio.Play(sound);
        theFade.FadeOut();
        yield return new WaitForSeconds(1f);
        thePlayer.startPointNumber = startPointNumber;
        thePlayer.currentMapName = transferMapName;
        SceneManager.LoadScene(transferMapName); // 이동할 맵의 이름으로 이동
        theFade.FadeIn();
        flag = false;


        theOrder.Move();
    }
}
