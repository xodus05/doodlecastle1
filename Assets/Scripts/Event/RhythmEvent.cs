using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RhythmEvent : MonoBehaviour
{
    [SerializeField]
    public Dialogue Dialogue_1;
    public Dialogue Dialogue_2;
    public Dialogue Dialogue_c;
    public Dialogue Dialogue_n;

    public Choice choice_1;

    private FadeManager theFade;
    private RhythmGame theRhythm;
    private OrderManager theOrder;
    private DialogueManager theDM;
    private CameraManager theCamera;
    private ChoiceManager theChoice;
    private BGMManager BGM;
    private AudioManager theAudio;
    private PlayerMove thePlayer;

    private string arrowPush;
    private string myAnswer; // 내가 입력한 값

    private bool flag;
    private bool flag2;
    private bool go;
    private int c = 2;

    public string sound;
    public string sound2;
    public int turn;
    public Text arrowText;

    public GameObject panel;

    
    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
        theRhythm = FindObjectOfType<RhythmGame>();
        theOrder = FindObjectOfType<OrderManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theCamera = FindObjectOfType<CameraManager>();
        BGM = FindObjectOfType<BGMManager>();
        theAudio = FindObjectOfType<AudioManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        arrowPush = "";
    }

    void Update()
    {
        if (flag2)
        {

            if (arrowPush.Length == c)
            {
                Debug.Log("내 답 : " + myAnswer);
                Debug.Log("정답 : " + theRhythm.correctNumber.ToString());
                go = true;
                arrowPush = "";
                flag2 = false;
                theAudio.Play(sound);
            }
            // Direction
            if (Input.GetKeyDown(KeyCode.UpArrow)) {    // 위 누르면
                arrowPush += "↑";   // 방향 저장
                myAnswer += "1";    // Answer에 추가
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                arrowPush += "↓";
                myAnswer += "2";
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                arrowPush += "←";
                myAnswer += "4";
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                arrowPush += "→";
                myAnswer += "3";
            }
            arrowText.text = arrowPush; // arrowText의 text가 화면에도 나오도록
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag)
        {
            flag = true;
            BGM.Play(3);
            BGM.FadeInMusic();
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기

        theOrder.NotMove();
        yield return new WaitForSeconds(0.1f);
        theDM.ShowDialogue(Dialogue_1);
        yield return new WaitUntil(()=>!theDM.talking);
        theChoice.ShowChoice(choice_1);
        yield return new WaitUntil(() => !theChoice.choiceIng);
        Debug.Log(theChoice.GetResult());
        switch (theChoice.GetResult())
        {
            case 0:
                panel.SetActive(true);
                for (int i = 0; i < turn; i++)
                {
                    go = false;
                    for (int j = 0; j < c; j++)
                    {
                        theAudio.Play(sound2);
                        theRhythm.createTile();
                        yield return new WaitForSeconds(1f);
                    }
                    flag2 = true;
                    yield return new WaitUntil(() => go);
                    if (theRhythm.correctNumber.ToString().Equals(myAnswer))
                    {
                        theFade.Flash();
                        theCamera.Shake();
                        theDM.ShowDialogue(Dialogue_c);
                        yield return new WaitForSeconds(1f);
                        theCamera.StopShake();
                        yield return new WaitUntil(() => !theDM.talking);
                        c++;
                    }
                    else
                    {
                        theDM.ShowDialogue(Dialogue_n);
                        yield return new WaitUntil(() => !theDM.talking);
                        i--;
                    }
                    myAnswer = "";
                    theRhythm.correctNumber = 0;
                    arrowText.text = arrowPush;
                }
                theDM.ShowDialogue(Dialogue_2);
                yield return new WaitUntil(() => !theDM.talking);
                SceneManager.LoadScene("house2");
                thePlayer.transform.position = new Vector2(-6049, 44);
                theOrder.Turn("player", "DOWN");
                theOrder.Move();
                break;
            case 1:
                SceneManager.LoadScene("end1");
                break;
        }
    }
}