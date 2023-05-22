using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class end1 : MonoBehaviour
{
    public Text text;
    public List<string> listSentences;

    private int count;
    private bool keyActivated = false;

    // Start is called before the first frame update
    void Start()
    {
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
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator StartChat()
    {        
        keyActivated = true;
        for (int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i];   // 한글자씩 출력
            yield return new WaitForSeconds(0.01f);
        }
        count++;
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
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(NormalChat());
                }
            }
        }
    }
}
