using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance;

    #region Singleton
    private void Awake() {
         if(instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
         }
         else {
            Destroy(this.gameObject);
         }
    }
    #endregion Singleton

    // public AudioManager theAudio;   // 사운드 재생

    private string question;
    private List<string> answerList;

    public GameObject go;   // 평소에 선택지를 비활성화 시킬 목적으로 선언

    public Text question_Text;

    public Text[] answer_Text;
    public GameObject[] answer_Panel;

    public Animator anim;

    // public string keySound;
    // public string enterSound;

    public bool choiceIng;  // 대기
    private bool keyInput;  // 키처리 활성화, 비활성화

    private int count;  // 배열 크기
    private int result; // 선택한 선택창

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    // Start is called before the first frame update
    void Start()
    {
        // theAudio = FindObjectOfType<AudioManager>();
        answerList = new List<string>();
        for(int i = 0; i <= 3 ; i++) {
            answer_Text[i].text = "";
            answer_Panel[i].SetActive(false);
        }
        question_Text.text = "";
    }

    public void ShowChoice(Choice _choice) {
        go.SetActive(true);
        choiceIng = true;
        result = 0;
        question = _choice.question;
        for(int i = 0; i < _choice.answers.Length; i++) {
            answerList.Add(_choice.answers[i]);
            answer_Panel[i].SetActive(true);
            count = i;
        }

        anim.SetBool("Appear", true);
        Selection();
        StartCoroutine(ChoiceCoroutine());
    }

    public int GetResult() {
        // go.SetActive(false);
        return result;
    }

    public void ExitChoice() {
        for(int i = 0; i <= count; i++) {
            answer_Text[i].text = "";
            answer_Panel[i].SetActive(false);
        }
        answerList.Clear();
        question_Text.text = "";
        choiceIng = false;
        go.SetActive(false);
    }

    IEnumerator ChoiceCoroutine() {
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(TypingQuestion());
        StartCoroutine(TypingAnswer(0));
        if(count >=1) StartCoroutine(TypingAnswer(1, 0.5f));
        if(count >=2) StartCoroutine(TypingAnswer(2, 0.6f));
        if(count >=3) StartCoroutine(TypingAnswer(3, 0.7f));

        yield return new WaitForSeconds(0.5f);
        keyInput = true;
    }

    IEnumerator TypingQuestion() {
        for(int i = 0; i < question.Length; i++) {
            question_Text.text += question[i];  // 한 글자씩 나오게
            yield return waitTime;
        }
    }
    IEnumerator TypingAnswer(int n, float time = 0.4f) {
        yield return new WaitForSeconds(time);
        for(int i = 0; i < answerList[n].Length; i++) {
            answer_Text[n].text += answerList[n][i];  // 한 글자씩 나오게
            yield return waitTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(keyInput) {
            if(Input.GetKeyDown(KeyCode.UpArrow)) {
                if(result > 0) result--;
                else result = count;
                Selection();
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow)) {
                if(result < count) result++;
                else result = 0;
                Selection();
            }
            else if(Input.GetKeyDown(KeyCode.Z)) {
                //theAudio.Play(enterSound);
                keyInput = false;
                ExitChoice();
            }
        }
    }

    public void Selection() {
        Color color = answer_Panel[0].GetComponent<Image>().color;
        color.a = 0.75f;
        for(int i = 0; i <=count; i++) {
            answer_Panel[i].GetComponent<Image>().color = color;
        }
        color.a = 1f;
        answer_Panel[result].GetComponent<Image>().color = color;
    }
}
