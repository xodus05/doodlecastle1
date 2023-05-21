using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rightOpenEvent : MonoBehaviour
{
   public string transferMapName; //이동할 맵의 이름
    public int startPointNumber;

    public string sound;

    private static bool flag;
    private static bool flag2;

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

       void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !flag && thePlayer.animator.GetFloat("DirX") == 1f && flag2)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
            flag2 = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
            flag2 = false;
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기
        theOrder.NotMove();
        yield return new WaitForSeconds(0.1f);
        theAudio.Play(sound);
        theFade.FadeOut();
        yield return new WaitForSeconds(1f);
        thePlayer.startPointNumber = startPointNumber;
        thePlayer.currentMapName = transferMapName;
        SceneManager.LoadScene(transferMapName); // 이동할 맵의 이름으로 이동
        theFade.FadeIn();
        flag = false;
        flag2 = false;

        theOrder.Move();
    }
}
