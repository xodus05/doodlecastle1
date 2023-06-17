using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class zoomin : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private BGMManager BGM;
    private CameraManager theCam;


    private bool flag;

    void Start()
    {
        BGM = FindObjectOfType<BGMManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theCam = FindObjectOfType<CameraManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (!flag)
            {
                flag = true;
/*                BGM.Play(5);
                BGM.FadeInMusic();*/
                StartCoroutine(EventCoroutine());
            }
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기

        theOrder.NotMove();
        yield return new WaitForSeconds(0.1f);
        /*        theDM.ShowDialogue(dialogue);
                yield return new WaitUntil(() => !theDM.talking);*/

        Vector3 zoomTarget = new Vector3(-5900,179); // 줌인할 좌표 설정
        theCam.ZoomIn(zoomTarget); // 해당 좌표로 카메라 줌인
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("end2");
    }
}
