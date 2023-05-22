using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class end1 : MonoBehaviour
{
    public Text text;
    public List<string> listSentences;

    private int count;
    public bool talking = false;
    private bool keyActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        text.text = "";
        listSentences = new List<string>();
        StartCoroutine(NormalChat());
    }

    IEnumerator NormalChat(string narrator, string narration)
    {
        for (int i = 0; i < listSentences.Length; i++)
        {

        }
    }

    void Update()
    {
        if (talking && keyActivated)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                theAudio.Play(enter_sound);
                keyActivated = false;
                text.text = "";     // text 초기화
                count++;
                if (count == listSentences.Count)
                {
                    StopAllCoroutines();
                    ExitDialogue();
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(StartDialogueCoroutine());
                }
            }
        }
    }
}
