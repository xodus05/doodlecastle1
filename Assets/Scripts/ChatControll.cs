using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChatControll : MonoBehaviour
{

    public Text text;
    [TextArea(1, 2)]
    public List<string> listSentences;

    private int count;
    private bool keyActivated = false;
    private bool isChatFinished = false;

    private FadeManager theFade;

    // Start is called before the first frame update
    void Start()
    {
        theFade = GetComponent<FadeManager>();
        count = 0;
        text.text = "";
        StartCoroutine(StartChat());
    }

    IEnumerator NormalChat()
    {
        keyActivated = true;
        for (int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i];   // 한글자씩 출력
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator StartChat()
    {
        keyActivated = true;
        for (int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i];   // 한글자씩 출력
            yield return new WaitForSeconds(0.2f);
        }
        // count++;
    }


    void Update()
    {
        if (keyActivated)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                keyActivated = false;
                text.text = "";     // text 초기화
                count++;
                if (count == listSentences.Count)
                {
                    StopAllCoroutines();
                    isChatFinished = true; // 대화가 모두 출력되었음을 표시
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(NormalChat());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("house"); //quote에서 house로 scene 이동
        }
    }

}
